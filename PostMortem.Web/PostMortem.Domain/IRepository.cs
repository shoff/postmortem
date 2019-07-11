namespace PostMortem.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Comments;
    using Polly;
    using Projects;
    using Questions;

    public interface IRepository
    {
        Task<PolicyResult<ICollection<Project>>> GetAllProjectsAsync();
        Task<PolicyResult> CreateProjectAsync(Project project);
        Task<Project> GetByProjectIdAsync(Guid projectId);
        Task<PolicyResult> AddCommentAsync(Comment comment);
        Task<PolicyResult> AddQuestionAsync(Question question);
        Task<PolicyResult> DeleteQuestionAsync(Guid questionId);
        Task<PolicyResult> DeleteCommentAsync(Guid questionId);
        Task<PolicyResult<ICollection<Question>>> GetQuestionsByProjectIdAsync(Guid projectId);
        Task<PolicyResult> UpdateCommentAsync(Comment comment);
        Task<PolicyResult> LikeCommentAsync(Guid commentId);
        Task<PolicyResult> DislikeCommentAsync(Guid commentId);
        Task<PolicyResult> UpdateQuestionAsync(Question question);
        Task<PolicyResult> UpdateProjectAsync(Project requestProject);
    }
}