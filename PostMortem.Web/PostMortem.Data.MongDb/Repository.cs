namespace PostMortem.Data.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Config;
    using Domain;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using Polly;

    public class Repository : IRepository
    {
        private readonly IMapper mapper;
        private readonly IOptions<MongoOptions> options;
        private readonly ILogger<Repository> logger;
        private readonly MongoClient client;
        private readonly IMongoDatabase database;


        public Repository(
            IMapper mapper,
            ILogger<Repository> logger,
            IOptions<MongoOptions> options)
        {
            this.mapper = Guard.IsNotNull(mapper, nameof(mapper));
            this.options = Guard.IsNotNull(options, nameof(options));
            this.logger = Guard.IsNotNull(logger, nameof(logger));

            MongoInternalIdentity internalIdentity = new MongoInternalIdentity(Constants.ADMIN, this.options.Value.Username);
            PasswordEvidence passwordEvidence = new PasswordEvidence(this.options.Value.Password);
            MongoCredential mongoCredential = new MongoCredential(this.options.Value.AuthMechanism, internalIdentity, passwordEvidence);

            MongoClientSettings settings = new MongoClientSettings
            {
                Credential = mongoCredential,
                Server = new MongoServerAddress(this.options.Value.MongoHost, int.Parse(this.options.Value.Port))
            };

            this.client = new MongoClient(settings);
            this.database = this.client.GetDatabase(this.options.Value.DefaultDb);
        }

        //        public async Task<PolicyResult<ICollection<Domain.Projects.Project>>> GetAllProjectsAsync()
        //        {
        //            var projects = this.database.GetCollection<Project>(Constants.PROJECTS_COLLECTION);
        //            var projectList = await projects.AsQueryable().ToListAsync();
        //            return projectList.Map(p => this.mapper.Map<Domain.Projects.Project>(p)).ToList();
        //        }

        //        public async Task<PolicyResult<Project>> GetByProjectIdAsync(Guid projectId)
        //        {
        //            var projects = this.database.GetCollection<Project>(Constants.PROJECTS_COLLECTION);
        //            var proj = await projects.FindAsync(p => p.ProjectId == projectId).ConfigureAwait(false);
        //            var project = proj.FirstOrDefault();

        //            if (project == null)
        //            {
        //                return (null);
        //            }

        //            var projectDto = this.mapper.Map<Domain.Projects.Project>(project);
        //            var questions = await this.GetQuestionsByProjectId(project.ProjectId);
        //#pragma warning disable 8123
        //            return projectDto;
        //#pragma warning restore 8123
        //        }

        //        public async Task<Domain.Projects.Project> CreateAsync(Project projectDto)
        //        {
        //            try
        //            {
        //                var project = this.mapper.Map<Project>(projectDto);
        //                var projects = this.database.GetCollection<Project>(Constants.PROJECTS_COLLECTION);
        //                await projects.InsertOneAsync(project);
        //                return this.mapper.Map<Domain.Projects.Project>(project);
        //            }
        //            catch (Exception e)
        //            {
        //                this.logger.LogError(e, e.Message);
        //                throw;
        //            }
        //        }

        //        public async Task<PolicyResult<ICollection<Question>>> GetQuestionsByProjectId(Guid projectId)
        //        {
        //            var questions = this.database.GetCollection<Question>(Constants.QUESTIONS_COLLECTION);
        //            var ques = await questions.FindAsync(f => f.ProjectId == projectId).ConfigureAwait(false);
        //            var ent = await ques.ToListAsync().ConfigureAwait(false);
        //            return ent.Map(q => this.mapper.Map<Question>(q)).ToList();
        //        }

        //        public async Task<PolicyResult<Question>> AddQuestionAsync(Question question)
        //        {
        //            Guard.IsNotNull(question, nameof(question));
        //            var questions = this.database.GetCollection<Question>(Constants.QUESTIONS_COLLECTION);
        //            var project = await this.GetByProjectIdAsync(question.ProjectId);

        //            if (project == null)
        //            {
        //                this.logger.LogError($"No project with the id {question.ProjectId}.");
        //                return new PolicyResult<Question>(System.Net.HttpStatusCode.NotFound, null);
        //            }

        //            var existing = questions.Find(q => q.QuestionId == question.QuestionId && q.ProjectId == question.ProjectId)
        //                .FirstOrDefault();

        //            if (existing == null)
        //            {
        //                await questions.InsertOneAsync(this.mapper.Map<Question>(question));
        //                return new PolicyResult<Question>(System.Net.HttpStatusCode.Created, question, true, $"api/v1/questions/{question.QuestionId}");
        //            }

        //            await questions.InsertOneAsync(this.mapper.Map<Question>(question));
        //            return new PolicyResult<Question>(System.Net.HttpStatusCode.Created, question, true);

        //        }

        //        public async Task<PolicyResult> AddCommentAsync(Comment comment)
        //        {
        //            Guard.IsNotNull(comment, nameof(comment));
        //            var questions = this.database.GetCollection<Question>(Constants.QUESTIONS_COLLECTION);
        //            var question = questions.Find(q => q.QuestionId == comment.QuestionId).FirstOrDefault();

        //            if (question != null)
        //            {
        //                question.Comments.Add(this.mapper.Map<Comment>(comment));
        //                var filter = Builders<Question>.Filter.Eq(q => q.QuestionId, comment.QuestionId);
        //                var result = await questions.ReplaceOneAsync(filter, question);
        //                if (result.IsAcknowledged && result.ModifiedCount > 0)
        //                {
        //                    return new ApiResult<CommentDto>(System.Net.HttpStatusCode.Accepted, comment);
        //                }

        //                return new ApiResult<CommentDto>(System.Net.HttpStatusCode.BadRequest, null);
        //            }

        //            this.logger.LogError($"No question with the id {comment.QuestionId} found to update.");
        //            return new ApiResult<CommentDto>(System.Net.HttpStatusCode.NotFound, null);

        //        }
        public Task<PolicyResult<ICollection<Domain.Projects.Project>>> GetAllProjectsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> CreateAsync(Domain.Projects.Project project)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Projects.Project> GetByProjectIdAsync(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> AddCommentAsync(Domain.Comments.Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> AddQuestionAsync(Domain.Questions.Question question)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> DeleteQuestionAsync(Guid questionId)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> DeleteCommentAsync(Guid questionId)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult<Domain.Questions.Question>> GetQuestionsByProjectIdAsync(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> UpdateCommentAsync(Domain.Comments.Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> LikeCommentAsync(Guid commentId)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> DislikeCommentAsync(Guid commentId)
        {
            throw new NotImplementedException();
        }
    }
}