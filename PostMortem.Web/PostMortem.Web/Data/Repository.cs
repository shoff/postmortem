namespace PostMortem.Web.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Config;
    using Dtos;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using Zatoichi.Common.Infrastructure.Extensions;
    using Zatoichi.Common.Infrastructure.Services;

    public class Repository : IRepository
    {
        private readonly IMapper mapper;
        private readonly IOptions<MongoOptions> options;
        private readonly ILogger<Repository> logger;
        private readonly MongoClient client;
        private readonly IMongoDatabase database;

        // ReSharper disable once InconsistentNaming
        private const string ADMIN = "admin";

        public Repository(
            IMapper mapper,
            ILogger<Repository> logger,
            IOptions<MongoOptions> options)
        {
            this.mapper = Guard.IsNotNull(mapper, nameof(mapper));
            this.options = Guard.IsNotNull(options, nameof(options));
            this.logger = Guard.IsNotNull(logger, nameof(logger));

            MongoInternalIdentity internalIdentity = new MongoInternalIdentity(ADMIN, this.options.Value.Username);
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

        public async Task<ICollection<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = this.database.GetCollection<Project>("projects");
            var projectList = await projects.AsQueryable().ToListAsync();
            return projectList.Map(p => this.mapper.Map<ProjectDto>(p)).ToList();
        }

        public async Task<(ProjectDto, ICollection<QuestionDto>)> GetByProjectIdAsync(Guid projectId)
        {
            var projects = this.database.GetCollection<Project>("projects");
            var proj = await projects.FindAsync(p => p.ProjectId == projectId).ConfigureAwait(false);
            var project = proj.FirstOrDefault();

            if (project == null)
            {
                return (null, new List<QuestionDto>());
            }

            var projectDto = this.mapper.Map<ProjectDto>(project);
            var questions = await this.GetQuestionsByProjectId(project.ProjectId);
#pragma warning disable 8123
            return (Project: projectDto, Questions: questions);
#pragma warning restore 8123
        }

        public async Task<ProjectDto> CreateAsync(CreateProjectDto projectDto)
        {
            try
            {
                var project = this.mapper.Map<Project>(projectDto);
                var projects = this.database.GetCollection<Project>("projects");
                await projects.InsertOneAsync(project);
                return this.mapper.Map<ProjectDto>(project);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task<ICollection<QuestionDto>> GetQuestionsByProjectId(Guid projectId)
        {
            var questions = this.database.GetCollection<Question>("questions");
            var ques = await questions.FindAsync(f => f.ProjectId == projectId).ConfigureAwait(false);
            var ent = await ques.ToListAsync().ConfigureAwait(false);
            return ent.Map(q => this.mapper.Map<QuestionDto>(q)).ToList();
        }

        public async Task<ApiResult<QuestionDto>> AddQuestionAsync(QuestionDto question)
        {
            Guard.IsNotNull(question, nameof(question));
            var questions = this.database.GetCollection<Question>("questions");
            var project = await this.GetByProjectIdAsync(question.ProjectId);

            if (project.Item1 == null)
            {
                this.logger.LogError($"No project with the id {question.ProjectId}.");
                return new ApiResult<QuestionDto>(System.Net.HttpStatusCode.NotFound, null);
            }

            var existing = questions.Find(q => q.QuestionId == question.QuestionId && q.ProjectId == question.ProjectId)
                .FirstOrDefault();

            if (existing == null)
            {
                await questions.InsertOneAsync(this.mapper.Map<Question>(question));
                return new ApiResult<QuestionDto>(System.Net.HttpStatusCode.Created, question, true, $"api/v1/questions/{question.QuestionId}");
            }

            await questions.InsertOneAsync(this.mapper.Map<Question>(question));
            return new ApiResult<QuestionDto>(System.Net.HttpStatusCode.Created, question, true);

        }

        public async Task<ApiResult<CommentDto>> AddCommentAsync(CommentDto comment)
        {
            Guard.IsNotNull(comment, nameof(comment));
            var questions = this.database.GetCollection<Question>("questions");
            var question = questions.Find(q => q.QuestionId == comment.QuestionId).FirstOrDefault();

            if (question != null)
            {
                question.Comments.Add(this.mapper.Map<Comment>(comment));
                var filter = Builders<Question>.Filter.Eq(q => q.QuestionId, comment.QuestionId);
                var result = await questions.ReplaceOneAsync(filter, question);
                if (result.IsAcknowledged && result.ModifiedCount > 0)
                {
                    return new ApiResult<CommentDto>(System.Net.HttpStatusCode.Accepted, comment);
                }

                return new ApiResult<CommentDto>(System.Net.HttpStatusCode.BadRequest, null);
            }

            this.logger.LogError($"No question with the id {comment.QuestionId} found to update.");
            return new ApiResult<CommentDto>(System.Net.HttpStatusCode.NotFound, null);

        }
    }
}