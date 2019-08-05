namespace PostMortem.Data.EventSourcing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Comments;
    using Domain.Projects;
    using Domain.Questions;
    using Zatoichi.EventSourcing;

    public class EventRepository : IRepository
    {
        private readonly IEventStore eventStore;

        public EventRepository(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public Task<ICollection<Project>> GetAllProjectsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateProjectAsync(Project project)
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetByProjectIdAsync(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> AddCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> AddQuestionAsync(Question question)
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

        public Task UpdateCommentAsync(Comment comment)
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

        public Task UpdateQuestionAsync(Question question)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProjectAsync(Project requestProject)
        {
            throw new NotImplementedException();
        }
    }
}