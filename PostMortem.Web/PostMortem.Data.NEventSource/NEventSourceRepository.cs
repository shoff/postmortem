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

    public partial class NEventSourceRepository : IRepository, IDisposable
    {
        public Task<PolicyResult<ICollection<Project>>> GetAllProjectsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResult> CreateAsync(Project project)
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetByProjectIdAsync(Guid projectId)
        {
            // acquire stream from store.
            // replay project stream in reverse, grab first event.
            throw new NotImplementedException();
        }

        public Task<PolicyResult> AddCommentAsync(Comment comment)
        {
            // create new stream for the comment. Add initial entry.
            throw new NotImplementedException();
        }

        public Task<PolicyResult> AddQuestionAsync(Question question)
        {
            // create new question 
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

        public void Dispose()
        {
        }
    }
}