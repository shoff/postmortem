namespace PostMortem.Data.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Config;
    using Domain.Comments.Events;
    using Domain.Questions;
    using Infrastructure;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
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

        private readonly InsertOneOptions doBypass = new InsertOneOptions {BypassDocumentValidation = true };
        private readonly InsertOneOptions doNotBypass = new InsertOneOptions {BypassDocumentValidation = false };
        private readonly InsertManyOptions doBypassMany = new InsertManyOptions { BypassDocumentValidation = true };
        private readonly InsertManyOptions doNotBypassMany = new InsertManyOptions { BypassDocumentValidation = false };


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
                Guard.IsNotNull(project, nameof(project));

                var projects = this.database.GetCollection<Project>(Constants.PROJECTS_COLLECTION);
                var mongoProject = this.mapper.Map<Project>(project);
                await projects.InsertOneAsync(mongoProject).ConfigureAwait(false);

            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
                throw;
            }

            return project.ProjectId.Id;
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

            var domainProject = new DomainProject(project.ProjectName, project.CreatedBy, project.StartDate, project.EndDate, project.ProjectId);
            domainProject.AddQuestions(questions);
            return domainProject;
        }

        public Task AddCommentAsync(CommentAdded comment, CancellationToken cancellationToken)
        {
            var collection = this.database.GetCollection<CommentAdded>(typeof(CommentAdded).Name);
            return collection.InsertOneAsync(comment, doBypass, cancellationToken);
        }

        public Task AddCommentsAsync(ICollection<CommentAdded> comments, CancellationToken cancellationToken)
        {
            var collection = this.database.GetCollection<CommentAdded>(typeof(CommentAdded).Name);
            return collection.InsertManyAsync(comments, doBypassMany, cancellationToken);
        }

        public Task AddCommentAsync(DomainComment domainComment)
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

        public Task AddQuestionAsync(DomainQuestion domainQuestion)
        {
            // TODO validate the projectId?
            try
            {
                var questionCollection = this.database.GetCollection<Question>(Constants.QUESTIONS_COLLECTION);
                var question = this.mapper.Map<Question>(domainQuestion);
                questionCollection.InsertOne(question);
                return Task.FromResult(question.Id);
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

        // CODE SMELL 
        public async Task<ICollection<DomainQuestion>> GetQuestionsByProjectIdAsync(Guid projectId)
        {
            var questionCollection = this.database.GetCollection<Question>(Constants.QUESTIONS_COLLECTION);
            var commentCollection = this.database.GetCollection<Comment>(Constants.COMMENTS_COLLECTION);
            var projectCollection = this.database.GetCollection<Project>(Constants.PROJECTS_COLLECTION);

            // TODO I hate this
            var projectCursor = await projectCollection.FindAsync(f => f.ProjectId == projectId).ConfigureAwait(false);
            var project = projectCursor.FirstOrDefault();

            var mongoQuestions = await questionCollection.FindAsync(f => f.ProjectId == projectId).ConfigureAwait(false);
            var questions = await mongoQuestions.ToListAsync();

            var domainProject = this.mapper.Map<DomainProject>(project);
            List<DomainQuestion> domainQuestions = new List<DomainQuestion>();

            foreach (var question in questions)
            {
                var mongoComments = await commentCollection.FindAsync(c => c.QuestionId == question.Id).ConfigureAwait(false);
                var comments = mongoComments.ToList().Map(c=> this.mapper.Map<DomainComment>(c)).ToList();

                throw new NotImplementedException();
                //var dc = new DomainQuestion(domainProject.GetOptions())
                //{
                //    Importance = question.Importance,
                //    ProjectId = question.ProjectId,
                //    QuestionId = question.Id,
                //    QuestionText = question.QuestionText,
                //    ResponseCount = question.ResponseCount
                //};
               // domainQuestions.Add(dc);
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
            await commentCollection.ReplaceOneAsync(filter, comment).ConfigureAwait(false);
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
            await commentCollection.ReplaceOneAsync(filter, comment).ConfigureAwait(false);
        }

        public Task UpdateQuestionAsync(DomainQuestion question)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProjectAsync(DomainProject requestProject)
        {
            throw new NotImplementedException();
        }

        public async Task<DomainQuestion> GetQuestionByIdAsync(Guid id, bool getSnapshot = false)
        {
            // here is where it get's tricky
            // the idea for the List<INotification>() is to add all events that
            // relate to the question, sort them, then add them in order, if I have time I'll pursue it later..
            // var events = new List<INotification>();
            
            // get the initial question
            var questionCursor = await Questions.FindAsync(q=>q.Id == id).ConfigureAwait(false);
            var question = questionCursor.FirstOrDefault();
            if (question == null)
            {
                throw new QuestionNotFoundException();
            }

            var questionEntity = this.mapper.Map<DomainQuestion>(question);

            // ok so I know there are comment events not sure if this the best way to approach this, guess I'll find out :)
            var commentsCursor = await this.CommentAddedEvents.FindAsync(c => c.QuestionId == id).ConfigureAwait(false);
            await commentsCursor.ForEachAsync(c =>
                questionEntity.AddComment(c.CommentText, c.Commenter, c.CommentId, c.ParentId));

            questionEntity.ClearPendingEvents();
            return questionEntity;
        }

        public IQueryable<T> All<T>() where T : class, new()
        {
            return this.database.GetCollection<T>(typeof(T).Name).AsQueryable();
        }

        public IQueryable<T> Where<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            Guard.IsNotNull(expression, nameof(expression));
            this.logger.LogDebug($"Where:{expression.Body}");
            return this.All<T>().Where(expression);
        }

        public void Delete<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            Guard.IsNotNull(predicate, nameof(predicate));
            this.logger.LogDebug($"Delete:{predicate.Body}");

            _ = this.database.GetCollection<T>(typeof(T).Name).DeleteMany(predicate);
        }

        public T Single<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            Guard.IsNotNull(expression, nameof(expression));
            this.logger.LogDebug($"Single:{expression.Body}");
            return this.All<T>().Where(expression).SingleOrDefault();
        }

        public bool CollectionExists<T>() where T : class, new()
        {
            this.logger.LogDebug($"CollectionExists:{typeof(T).Name}");

            return this.CollectionExists<T>(typeof(T).Name);
        }

        public bool CollectionExists<T>(string collectionName) where T : class, new()
        {
            Guard.IsNotNullOrWhitespace(collectionName, nameof(collectionName));
            var collection = this.database.GetCollection<T>(collectionName);
            var filter = new BsonDocument();
            var totalCount = collection.CountDocuments(filter);
            return totalCount > 0;
        }

        public void Add<T>(T item) where T : class, new()
        {
            Guard.IsNotDefault(item, nameof(item));
            this.logger.LogDebug($"Add:{typeof(T).Name} {item.ToJson()}");
            this.database.GetCollection<T>(typeof(T).Name).InsertOne(item);
        }

        public void Add<T>(IEnumerable<T> items) where T : class, new()
        {
            // ReSharper disable PossibleMultipleEnumeration
            Guard.IsNotNullOrEmpty(items, nameof(items));
            this.database.GetCollection<T>(typeof(T).Name).InsertMany(items);
            // ReSharper restore PossibleMultipleEnumeration
        }

        private IMongoCollection<Question> Questions => this.database.GetCollection<Question>(Constants.QUESTIONS_COLLECTION);
        private IMongoCollection<Project> Projects => this.database.GetCollection<Project>(Constants.PROJECTS_COLLECTION);
        private IMongoCollection<CommentAdded> CommentAddedEvents => this.database.GetCollection<CommentAdded>(typeof(CommentAdded).Name);
        private IMongoDatabase Database => this.database;
    }
}