namespace PostMortem.Data.EventSourcing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Comments;
    using Domain.Projects;
    using Domain.Questions;
    using Polly;

    public partial class EventRepository : IRepository
    {
        public Task<PolicyResult<ICollection<Project>>> GetAllProjectsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> CreateProjectAsync(Project project)
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetByProjectIdAsync(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> AddCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> AddQuestionAsync(Question question)
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

        public Task<PolicyResult<Question>> GetQuestionsByProjectIdAsync(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> UpdateCommentAsync(Comment comment)
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

        public Task<PolicyResult> UpdateQuestionAsync(Question question)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> UpdateProjectAsync(Project requestProject)
        {
            throw new NotImplementedException();
        }
    }
}