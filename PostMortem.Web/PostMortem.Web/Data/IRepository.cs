namespace PostMortem.Web.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dtos;
    using Zatoichi.Common.Infrastructure.Services;

    public interface IRepository
    {
        Task<ICollection<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto> CreateAsync(CreateProjectDto project);
        Task<(ProjectDto, ICollection<QuestionDto>)> GetByProjectIdAsync(Guid projectId);
        Task<ApiResult<CommentDto>> AddCommentAsync(CommentDto comment);
        Task<ApiResult<QuestionDto>> AddQuestionAsync(QuestionDto question);
        Task<ICollection<QuestionDto>> GetQuestionsByProjectId(Guid projectId);
    }
}