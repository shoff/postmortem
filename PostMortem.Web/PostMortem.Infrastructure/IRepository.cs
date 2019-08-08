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
        Task<ICollection<Project>> GetAllProjectsAsync(CancellationToken cancellationToken);
        Task<Guid> CreateProjectAsync(Project project, CancellationToken cancellationToken);
        Task<Project> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken);

        // Comments can only be added through the question as events
        Task AddCommentAsync(CommentAdded comment, CancellationToken cancellationToken);
        Task AddCommentsAsync(ICollection<CommentAdded> comments, CancellationToken cancellationToken);
        Task AddQuestionAsync(Question question, CancellationToken cancellationToken);
        Task DeleteQuestionAsync(Guid questionId, CancellationToken cancellationToken);
        Task DeleteCommentAsync(Guid questionId, CancellationToken cancellationToken);
        Task<ICollection<Question>> GetQuestionsByProjectIdAsync(Guid projectId, CancellationToken cancellationToken);
        Task UpdateCommentAsync(Comment comment, CancellationToken cancellationToken);
        Task LikeCommentAsync(Guid commentId, CancellationToken cancellationToken);
        Task DislikeCommentAsync(Guid commentId, CancellationToken cancellationToken);
        Task UpdateQuestionAsync(Question question, CancellationToken cancellationToken);
        Task UpdateProjectAsync(Project requestProject, CancellationToken cancellationToken);
        Task<Question> GetQuestionByIdAsync(Guid id, CancellationToken cancellationToken, bool getSnapshot = false);
    }
}