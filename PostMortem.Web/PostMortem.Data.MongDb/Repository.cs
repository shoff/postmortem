namespace PostMortem.Data.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Config;
    using Domain;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using Zatoichi.Common.Infrastructure.Extensions;

    using DomainProject = Domain.Projects.Project;
    using DomainQuestion = Domain.Questions.Question;
    using DomainComment = Domain.Comments.Comment;

    public class Repository : IRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<Repository> logger;
        private readonly IMongoDatabase database;
        
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
                Server = new MongoServerAddress(options.Value.MongoHost, int.Parse(options.Value.Port))
            };

            var client = new MongoClient(settings);
            this.database = client.GetDatabase(options.Value.DefaultDb);
        }

        public async Task<ICollection<DomainProject>> GetAllProjectsAsync()
        {
            var projects = this.database.GetCollection<Project>(Constants.PROJECTS_COLLECTION);
            var projectList = await projects.AsQueryable().ToListAsync();
            return projectList.Map(p => this.mapper.Map<DomainProject>(p)).ToList();
        }

        public async Task<Guid> CreateProjectAsync(DomainProject project)
        {
            try
            {
                var projects = this.database.GetCollection<Project>(Constants.PROJECTS_COLLECTION);
                var mongoProject = this.mapper.Map<Project>(project);
                await projects.InsertOneAsync(mongoProject);
                return project.ProjectId;
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
                throw;
            }  
        }

        public async Task<DomainProject> GetByProjectIdAsync(Guid projectId)
        {
            // https://docs.mongodb.com/manual/tutorial/model-referenced-one-to-many-relationships-between-documents/
            var projects = this.database.GetCollection<Project>(Constants.PROJECTS_COLLECTION);
            var proj = await projects.FindAsync(p => p.ProjectId == projectId).ConfigureAwait(false);
            var project = proj.FirstOrDefault();

            if (project == null)
            {
                return (null);
            }
            var questions = await this.GetQuestionsByProjectIdAsync(project.ProjectId);
            var domainProject = new DomainProject(questions)
            {
                EndDate = project.EndDate,
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                StartDate = project.StartDate
            };
            return domainProject;
        }

        public Task<Guid> AddCommentAsync(DomainComment domainComment)
        {
            Guard.IsNotNull(domainComment, nameof(domainComment));
            try
            {
                var commentsCollection = this.database.GetCollection<Comment>(Constants.COMMENTS_COLLECTION);
                var comment = this.mapper.Map<Comment>(domainComment);
                commentsCollection.InsertOne(comment);
                return Task.FromResult(comment.CommentId);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
                throw;
            }
        }

        public Task<Guid> AddQuestionAsync(DomainQuestion domainQuestion)
        {
            // TODO validate the projectId?
            try
            {
                var questionCollection = this.database.GetCollection<Question>(Constants.QUESTIONS_COLLECTION);
                var question = this.mapper.Map<Question>(domainQuestion);
                questionCollection.InsertOne(question);
                return Task.FromResult(question.QuestionId);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
                throw;
            }
        }

        public Task DeleteQuestionAsync(Guid questionId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCommentAsync(Guid questionId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<DomainQuestion>> GetQuestionsByProjectIdAsync(Guid projectId)
        {
            var questionCollection = this.database.GetCollection<Question>(Constants.QUESTIONS_COLLECTION);
            var commentCollection = this.database.GetCollection<Comment>(Constants.COMMENTS_COLLECTION);

            var mongoQuestions = await questionCollection.FindAsync(f => f.ProjectId == projectId).ConfigureAwait(false);
            var questions = await mongoQuestions.ToListAsync();

            List<DomainQuestion> domainQuestions = new List<DomainQuestion>();

            foreach (var question in questions)
            {
                var mongoComments = await commentCollection.FindAsync(c => c.QuestionId == question.QuestionId).ConfigureAwait(false);
                var comments = mongoComments.ToList().Map(c=> this.mapper.Map<DomainComment>(c)).ToList();
                var dc = new DomainQuestion(comments)
                {
                    Importance = question.Importance,
                    ProjectId = question.ProjectId,
                    QuestionId = question.QuestionId,
                    QuestionText = question.QuestionText,
                    ResponseCount = question.ResponseCount
                };
                domainQuestions.Add(dc);
            }

            return domainQuestions;
        }

        public Task UpdateCommentAsync(DomainComment comment)
        {
            throw new NotImplementedException();
        }

        public async Task LikeCommentAsync(Guid commentId)
        {
            var commentCollection = this.database.GetCollection<Comment>(Constants.COMMENTS_COLLECTION);
            var mongoComment = await commentCollection.FindAsync(c => c.CommentId == commentId).ConfigureAwait(false);
            
            var comment = mongoComment.FirstOrDefault();
            if (comment == null)
            {
                // meh this is a POC so..
                return;
            }

            comment.Likes++;
            var filter = Builders<Comment>.Filter.Eq("comment_id", commentId);
            await commentCollection.ReplaceOneAsync(filter, comment);
        }

        public async Task DislikeCommentAsync(Guid commentId)
        {
            var commentCollection = this.database.GetCollection<Comment>(Constants.COMMENTS_COLLECTION);
            var mongoComment = await commentCollection.FindAsync(c => c.CommentId == commentId).ConfigureAwait(false);

            var comment = mongoComment.FirstOrDefault();
            if (comment == null)
            {
                // meh this is a POC so..
                return;
            }

            comment.Likes--;
            var filter = Builders<Comment>.Filter.Eq("comment_id", commentId);
            await commentCollection.ReplaceOneAsync(filter, comment);
        }

        public Task UpdateQuestionAsync(DomainQuestion question)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProjectAsync(DomainProject requestProject)
        {
            throw new NotImplementedException();
        }
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

    //        public async Task<Domain.Projects.Project> CreateProjectAsync(Project projectDto)
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
}