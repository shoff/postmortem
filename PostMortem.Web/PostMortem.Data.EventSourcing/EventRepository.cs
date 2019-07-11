namespace PostMortem.Data.EventSourcing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Comments;
    using Domain.Events;
    using Domain.Questions;

    using DomainProject = Domain.Projects.Project;
    using DomainQuestion = Domain.Questions.Question;
    using DomainComment = Domain.Comments.Comment;

    public class EventRepository : IRepository
    {
        private IEventBroker eventBroker;

        public EventRepository(IEventBroker eventBroker)
        {
            this.eventBroker = eventBroker;
        }

        public Task<ICollection<DomainProject>> GetAllProjectsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateProjectAsync(DomainProject project)
        {
            throw new NotImplementedException();
        }

        public Task<DomainProject> GetByProjectIdAsync(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> AddCommentAsync(DomainComment comment)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> AddQuestionAsync(DomainQuestion question)
        {
            throw new NotImplementedException();
        }

        public Task DeleteQuestionAsync(Guid questionId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCommentAsync(Guid questionId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Question>> GetQuestionsByProjectIdAsync(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCommentAsync(DomainComment comment)
        {
            throw new NotImplementedException();
        }

        public Task LikeCommentAsync(Guid commentId)
        {
            throw new NotImplementedException();
        }

        public Task DislikeCommentAsync(Guid commentId)
        {
            throw new NotImplementedException();
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
}