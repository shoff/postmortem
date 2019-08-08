namespace PostMortem.Data.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Config;
    using Domain.Questions;
    using Infrastructure;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using Zatoichi.Common.Infrastructure.Extensions;
    using Zatoichi.EventSourcing;
    using DomainProject = Domain.Projects.Project;
    using DomainQuestion = Domain.Questions.Question;

    public class Repository : IRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<Repository> logger;
        private readonly IMongoDatabase database;
        private readonly InsertOneOptions doBypass = new InsertOneOptions { BypassDocumentValidation = true };
        private readonly InsertManyOptions doBypassMany = new InsertManyOptions { BypassDocumentValidation = true };


        public Repository(
            IMapper mapper,
            ILogger<Repository> logger,
            IOptions<MongoOptions> options)
        {
            Guard.IsNotNull(options, nameof(options));
            this.mapper = Guard.IsNotNull(mapper, nameof(mapper));
            this.logger = Guard.IsNotNull(logger, nameof(logger));

            MongoInternalIdentity internalIdentity = new MongoInternalIdentity(Constants.ADMIN, options.Value.Username);
            PasswordEvidence passwordEvidence = new PasswordEvidence(options.Value.Password);
            MongoCredential mongoCredential = new MongoCredential(options.Value.AuthMechanism, internalIdentity, passwordEvidence);

            MongoClientSettings settings = new MongoClientSettings
            {
                Credential = mongoCredential,
                Server = new MongoServerAddress(options.Value.MongoHost, int.Parse(options.Value.Port)),
                GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard
            };

            var client = new MongoClient(settings);
            this.database = client.GetDatabase(options.Value.DefaultDb);
        }

        public async Task<ICollection<DomainProject>> GetAllProjectsAsync(CancellationToken cancellationToken)
        {
            var projects = this.database.GetCollection<Project>(Constants.PROJECTS_COLLECTION);
            var projectList = await projects.AsQueryable().ToListAsync(cancellationToken);
            return projectList.Map(p => this.mapper.Map<DomainProject>(p)).ToList();
        }

        public async Task<Guid> CreateProjectAsync(DomainProject project, CancellationToken cancellationToken)
        {
            try
            {
                Guard.IsNotNull(project, nameof(project));
                var mongoProject = this.mapper.Map<Project>(project);
                await this.Projects.InsertOneAsync(mongoProject, this.doBypass, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
                throw;
            }

            return project.ProjectId.Id;
        }

        public async Task<DomainProject> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken)
        {
            // https://docs.mongodb.com/manual/tutorial/model-referenced-one-to-many-relationships-between-documents/
            var projects = this.database.GetCollection<Project>(Constants.PROJECTS_COLLECTION);
            var proj = await projects.FindAsync(p => p.ProjectId == projectId, cancellationToken: cancellationToken).ConfigureAwait(false);
            var project = proj.FirstOrDefault();

            if (project == null)
            {
                return (null);
            }
            var questions = await this.GetQuestionsByProjectIdAsync(project.ProjectId, false, cancellationToken);
            var domainProject = new DomainProject(project.ProjectName, project.CreatedBy, project.StartDate, project.EndDate, project.ProjectId);
            domainProject.AddQuestions(questions);
            return domainProject;
        }

        public Task UpdateProjectAsync(DomainProject requestProject, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
      
        // CODE SMELL 
        public async Task<ICollection<DomainQuestion>> GetQuestionsByProjectIdAsync(Guid projectId, bool replay, CancellationToken cancellationToken)
        {
            var mongoQuestions = await this.Questions.FindAsync(f => f.ProjectId == projectId, cancellationToken: cancellationToken).ConfigureAwait(false);
            var questions = await mongoQuestions.ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            List<DomainQuestion> domainQuestions = questions.Map(q => this.mapper.Map<DomainQuestion>(q)).ToList();

            if (replay)
            {
                foreach (var question in domainQuestions)
                {
                    var lastCommit = (from quest in questions
                        where quest.QuestionId == question.QuestionId.Id
                        select quest.CommitDate).First();

                    var events = this.mapper.ProjectTo<DomainEvent>
                        (this.DomainEvents.AsQueryable().Where(d => d.CommitDate > lastCommit)).ToList();

                    question.ApplyEvents(events);
                }
            }
            return domainQuestions;
        }

        public async Task UpdateQuestionAsync(DomainQuestion question, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(question, nameof(question));

            var esEvents = question.DomainEvents.Map(d => this.mapper.Map<EsEvent>(d)).ToList();

            await this.DomainEvents.InsertManyAsync(esEvents, this.doBypassMany, cancellationToken).ConfigureAwait(false);

            await this.Questions.ReplaceOneAsync(q => q.QuestionId == question.QuestionId.Id,
                this.mapper.Map<Question>(question), cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public Task<DomainQuestion> GetQuestionByIdAsync(Guid id, CancellationToken cancellationToken,
            bool getSnapshot = false)
        {
            var question = this.Questions.AsQueryable().FirstOrDefault(q => q.QuestionId == id);
            if (question == null)
            {
                throw new QuestionNotFoundException();
            }
            // get the initial question
            var questionEntity = this.mapper.Map<DomainQuestion>(question);

            var domainEvents = this.DomainEvents.AsQueryable()
                .Where(de => de.QuestionId == id).ToList();

            questionEntity.ApplyEvents(domainEvents.Map(d => this.mapper.Map<DomainEvent>(d)).ToList());
            questionEntity.ClearPendingEvents();
            return Task.FromResult(questionEntity);
        }

        public Task AddQuestionAsync(DomainQuestion domainQuestion, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(domainQuestion, nameof(domainQuestion));
            try
            {
                var question = this.mapper.Map<Question>(domainQuestion);
                return this.Questions.InsertOneAsync(question, this.doBypass, cancellationToken);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
                throw;
            }
        }

        public Task DeleteQuestionAsync(Guid questionId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCommentAsync(Guid questionId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        
        private IMongoCollection<EsEvent> DomainEvents => this.database.GetCollection<EsEvent>(Constants.DOMAIN_EVENTS_COLLECTION);
        private IMongoCollection<Question> Questions => this.database.GetCollection<Question>(Constants.QUESTIONS_COLLECTION);
        private IMongoCollection<Project> Projects => this.database.GetCollection<Project>(Constants.PROJECTS_COLLECTION);
    }
}