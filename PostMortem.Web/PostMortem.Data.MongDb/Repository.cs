using PostMortem.Domain.Projects;
using PostMortem.Domain.Questions;

namespace PostMortem.Data.MongoDb
{
    //using System;
    //using System.Collections.Generic;
    //using System.Linq;
    //using System.Threading.Tasks;
    //using AutoMapper;
    //using ChaosMonkey.Guards;
    //using Config;
    //using Domain;
    //using Microsoft.Extensions.Logging;
    //using Microsoft.Extensions.Options;
    //using MongoDB.Driver;
    //using Zatoichi.Common.Infrastructure.Extensions;

    //using DomainProject = Domain.Projects.Project;
    //using DomainQuestion = Domain.Questions.Question;
    //using DomainComment = Domain.Comments.Comment;

    //public class Repository : IRepository
    //{
    //    private readonly IMapper mapper;
    //    private readonly ILogger<Repository> logger;
    //    private readonly IMongoDatabase database;

    //    public Repository(
    //        IMapper mapper,
    //        ILogger<Repository> logger,
    //        IOptions<MongoOptions> options)
    //    {
    //        Guard.IsNotNull(options, nameof(options));
    //        this.mapper = Guard.IsNotNull(mapper, nameof(mapper));
    //        this.logger = Guard.IsNotNull(logger, nameof(logger));

    //        MongoInternalIdentity internalIdentity = new MongoInternalIdentity(Constants.ADMIN, options.Value.Username);
    //        PasswordEvidence passwordEvidence = new PasswordEvidence(options.Value.Password);
    //        MongoCredential mongoCredential = new MongoCredential(options.Value.AuthMechanism, internalIdentity, passwordEvidence);

    //        MongoClientSettings settings = new MongoClientSettings
    //        {
    //            Credential = mongoCredential,
    //            Server = new MongoServerAddress(options.Value.MongoHost, int.Parse(options.Value.Port))
    //        };

    //        var client = new MongoClient(settings);
    //        this.database = client.GetDatabase(options.Value.DefaultDb);
    //    }

    //    public async Task<ICollection<DomainProject>> GetAllProjectsAsync()
    //    {
    //        var projects = this.database.GetCollection<Project>(Constants.PROJECTS_COLLECTION);
    //        var projectList = await projects.AsQueryable().ToListAsync();
    //        return projectList.Map(p => this.mapper.Map<DomainProject>(p)).ToList();
    //    }

    //    public async Task<Guid> CreateProjectAsync(DomainProject project)
    //    {
    //        try
    //        {
    //            var projects = this.database.GetCollection<Project>(Constants.PROJECTS_COLLECTION);
    //            var mongoProject = this.mapper.Map<Project>(project);
    //            await projects.InsertOneAsync(mongoProject);
    //            return project.ProjectId.Id;
    //        }
    //        catch (Exception e)
    //        {
    //            this.logger.LogError(e, e.Message);
    //            throw;
    //        }
    //    }

    //    public async Task<DomainProject> GetByProjectIdAsync(Guid projectId)
    //    {
    //        // https://docs.mongodb.com/manual/tutorial/model-referenced-one-to-many-relationships-between-documents/
    //        var projects = this.database.GetCollection<Project>(Constants.PROJECTS_COLLECTION);
    //        var proj = await projects.FindAsync(p => p.ProjectId == projectId).ConfigureAwait(false);
    //        var project = proj.FirstOrDefault();

    //        if (project == null)
    //        {
    //            return (null);
    //        }
    //        var questions = await this.GetQuestionsByProjectIdAsync(project.ProjectId);
    //        var domainProject = new DomainProject(questions)
    //        {
    //            EndDate = project.EndDate,
    //            ProjectId = new ProjectId(project.ProjectId),
    //            ProjectName = project.ProjectName,
    //            StartDate = project.StartDate
    //        };
    //        return domainProject;
    //    }

    //    public async Task<ICollection<DomainComment>> GetCommentsByQuestionIdAsync(Guid questionId)
    //    {
    //        var commentCollection = this.database.GetCollection<Comment>(Constants.COMMENTS_COLLECTION);
    //        var mongoComments = await commentCollection.FindAsync(c => c.QuestionId == questionId).ConfigureAwait(false);
    //        return mongoComments.ToList().Map(c => this.mapper.Map<DomainComment>(c)).ToList();
    //    }

    //    public async Task<DomainQuestion> GetQuestionByIdAsync(Guid questionId, bool includeComments = false)
    //    {
    //        // https://docs.mongodb.com/manual/tutorial/model-referenced-one-to-many-relationships-between-documents/
    //        var questions = this.database.GetCollection<Question>(Constants.QUESTIONS_COLLECTION);
    //        var mongoQuestion = await questions.FindAsync(p => p.QuestionId == questionId).ConfigureAwait(false);
    //        var question = mongoQuestion.FirstOrDefault();

    //        if (question == null)
    //        {
    //            return null;
    //        }
    //        if (!includeComments)
    //        {
    //            var domainQuestion = new DomainQuestion
    //            {
    //                Active = question.Active,
    //                Importance = question.Importance,
    //                ProjectId = question.ProjectId,
    //                QuestionId = new QuestionId(question.QuestionId),
    //                QuestionText = question.QuestionText,
    //                ResponseCount = question.ResponseCount
    //            };
    //            return domainQuestion;
    //        }


    //        var comments = await this.GetCommentsByQuestionIdAsync(questionId);

    //        return new DomainQuestion(comments)
    //        {
    //            Active = question.Active,
    //            Importance = question.Importance,
    //            ProjectId = question.ProjectId,
    //            QuestionId = new QuestionId(question.QuestionId),
    //            QuestionText = question.QuestionText,
    //            ResponseCount = question.ResponseCount
    //        };
    //    }

    //    public Task<Guid> AddCommentAsync(DomainComment domainComment)
    //    {
    //        Guard.IsNotNull(domainComment, nameof(domainComment));
    //        try
    //        {
    //            var commentsCollection = this.database.GetCollection<Comment>(Constants.COMMENTS_COLLECTION);
    //            var comment = this.mapper.Map<Comment>(domainComment);
    //            commentsCollection.InsertOne(comment);
    //            return Task.FromResult(comment.CommentId);
    //        }
    //        catch (Exception e)
    //        {
    //            this.logger.LogError(e, e.Message);
    //            throw;
    //        }
    //    }

    //    public Task<Guid> AddQuestionAsync(DomainQuestion domainQuestion)
    //    {
    //        // TODO validate the projectId?
    //        try
    //        {
    //            var questionCollection = this.database.GetCollection<Question>(Constants.QUESTIONS_COLLECTION);
    //            var question = this.mapper.Map<Question>(domainQuestion);
    //            questionCollection.InsertOne(question);
    //            return Task.FromResult(question.QuestionId);
    //        }
    //        catch (Exception e)
    //        {
    //            this.logger.LogError(e, e.Message);
    //            throw;
    //        }
    //    }

    //    public Task DeleteQuestionAsync(Guid questionId)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task DeleteCommentAsync(Guid questionId)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<ICollection<DomainQuestion>> GetQuestionsByProjectIdAsync(Guid projectId)
    //    {
    //        var questionCollection = this.database.GetCollection<Question>(Constants.QUESTIONS_COLLECTION);
    //        var commentCollection = this.database.GetCollection<Comment>(Constants.COMMENTS_COLLECTION);

    //        var mongoQuestions = await questionCollection.FindAsync(f => f.ProjectId == projectId).ConfigureAwait(false);
    //        var questions = await mongoQuestions.ToListAsync();

    //        List<DomainQuestion> domainQuestions = new List<DomainQuestion>();

    //        foreach (var question in questions)
    //        {
    //            var mongoComments = await commentCollection.FindAsync(c => c.QuestionId == question.QuestionId).ConfigureAwait(false);
    //            var comments = mongoComments.ToList().Map(c => this.mapper.Map<DomainComment>(c)).ToList();
    //            var dc = new DomainQuestion(comments)
    //            {
    //                Importance = question.Importance,
    //                ProjectId = question.ProjectId,
    //                QuestionId = new QuestionId(question.QuestionId),
    //                QuestionText = question.QuestionText,
    //                ResponseCount = question.ResponseCount
    //            };
    //            domainQuestions.Add(dc);
    //        }

    //        return domainQuestions;
    //    }

    //    public Task UpdateCommentAsync(DomainComment comment)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task LikeCommentAsync(Guid commentId)
    //    {
    //        var commentCollection = this.database.GetCollection<Comment>(Constants.COMMENTS_COLLECTION);
    //        var mongoComment = await commentCollection.FindAsync(c => c.CommentId == commentId).ConfigureAwait(false);

    //        var comment = mongoComment.FirstOrDefault();
    //        if (comment == null)
    //        {
    //            // meh this is a POC so..
    //            return;
    //        }

    //        comment.Likes++;
    //        var filter = Builders<Comment>.Filter.Eq("comment_id", commentId);
    //        await commentCollection.ReplaceOneAsync(filter, comment);
    //    }

    //    public async Task DislikeCommentAsync(Guid commentId)
    //    {
    //        var commentCollection = this.database.GetCollection<Comment>(Constants.COMMENTS_COLLECTION);
    //        var mongoComment = await commentCollection.FindAsync(c => c.CommentId == commentId).ConfigureAwait(false);

    //        var comment = mongoComment.FirstOrDefault();
    //        if (comment == null)
    //        {
    //            // meh this is a POC so..
    //            return;
    //        }

    //        comment.Likes--;
    //        var filter = Builders<Comment>.Filter.Eq("comment_id", commentId);
    //        await commentCollection.ReplaceOneAsync(filter, comment);
    //    }

    //    public Task UpdateQuestionAsync(DomainQuestion question)
    //    {
    //        var questionCollection = this.database.GetCollection<Question>(Constants.QUESTIONS_COLLECTION);
    //        return questionCollection.ReplaceOneAsync<Question>
    //            (q=>q.QuestionId == question.QuestionId.Id, this.mapper.Map<Question>(question));
    //    }

    //    public Task UpdateProjectAsync(DomainProject requestProject)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task UpdateQuestionResponseCount(Guid questionId, int count)
    //    {
    //        var questionCollection = this.database.GetCollection<Question>(Constants.QUESTIONS_COLLECTION);
    //        return questionCollection.UpdateOneAsync(
    //            Builders<Question>.Filter.Eq("question_id", questionId),
    //            Builders<Question>.Update.Set("response_count", count));
    //    }
    //    public Task UpdateQuestionImportance(Guid questionId, int count)
    //    {
    //        var questionCollection = this.database.GetCollection<Question>(Constants.QUESTIONS_COLLECTION);
    //        return questionCollection.UpdateOneAsync(
    //            Builders<Question>.Filter.Eq("question_id", questionId),
    //            Builders<Question>.Update.Set("importance", count));
    //    }

    //}
}