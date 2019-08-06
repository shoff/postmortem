namespace PostMortem.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Comments;
    using Projects;
    using Questions;

    public interface IRepository
    {
        Task<ICollection<Project>> GetAllProjectsAsync();
        Task<Guid> CreateProjectAsync(Project project);
        Task<Project> GetByProjectIdAsync(Guid projectId);
        Task<Guid> AddCommentAsync(Comment comment);
        Task<Guid> AddQuestionAsync(Question question);
        Task DeleteQuestionAsync(Guid questionId);
        Task DeleteCommentAsync(Guid questionId);
        Task<ICollection<Question>> GetQuestionsByProjectIdAsync(Guid projectId);
        Task UpdateCommentAsync(Comment comment);
        Task LikeCommentAsync(Guid commentId);
        Task DislikeCommentAsync(Guid commentId);
        Task UpdateQuestionAsync(Question question);
        Task UpdateProjectAsync(Project requestProject);
        Task<Question> GetQuestionByIdAsync(Guid id);
    }
}