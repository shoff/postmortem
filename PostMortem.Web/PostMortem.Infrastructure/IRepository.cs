namespace PostMortem.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Projects;
    using Domain.Questions;

    public interface IRepository
    {
        Task<ICollection<Project>> GetAllProjectsAsync(CancellationToken cancellationToken);
        Task<Guid> CreateProjectAsync(Project project, CancellationToken cancellationToken);
        Task<Project> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken);
        Task AddQuestionAsync(Question question, CancellationToken cancellationToken);
        Task DeleteQuestionAsync(Guid questionId, CancellationToken cancellationToken);
        Task DeleteCommentAsync(Guid questionId, CancellationToken cancellationToken);
        Task<ICollection<Question>> GetQuestionsByProjectIdAsync(Guid projectId, bool replay, CancellationToken cancellationToken);
        Task UpdateQuestionAsync(Question question, CancellationToken cancellationToken);
        Task UpdateProjectAsync(Project requestProject, CancellationToken cancellationToken);
        Task<Question> GetQuestionByIdAsync(Guid id, CancellationToken cancellationToken, bool getSnapshot = false);
    }
}