namespace PostMortem.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Comments;
    using Domain.Comments.Events;
    using Domain.Projects;
    using Domain.Questions;

    public interface IRepository
    {
        Task<ICollection<Project>> GetAllProjectsAsync();
        Task<Guid> CreateProjectAsync(Project project);
        Task<Project> GetByProjectIdAsync(Guid projectId);

        // Comments can only be added through the question as events
        Task AddCommentAsync(CommentAdded comment, CancellationToken cancellationToken);
        Task AddCommentsAsync(ICollection<CommentAdded> comments, CancellationToken cancellationToken);
        Task AddQuestionAsync(Question question);
        Task DeleteQuestionAsync(Guid questionId);
        Task DeleteCommentAsync(Guid questionId);
        Task<ICollection<Question>> GetQuestionsByProjectIdAsync(Guid projectId);
        Task UpdateCommentAsync(Comment comment);
        Task LikeCommentAsync(Guid commentId);
        Task DislikeCommentAsync(Guid commentId);
        Task UpdateQuestionAsync(Question question, CancellationToken cancellationToken);
        Task UpdateProjectAsync(Project requestProject);
        Task<Question> GetQuestionByIdAsync(Guid id, bool getSnapshot = false);
    }
}