namespace PostMortem.Web.Converters
{
    using System;
    using AutoMapper;
    using Data;
    using Dtos;

    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            this.CreateMap<Comment, CommentDto>().ConvertUsing<CommentConverter>();
            this.CreateMap<Question, QuestionDto>().ConvertUsing<QuestionConverter>();
            this.CreateMap<Project, ProjectDto>();
            this.CreateMap<CreateProjectDto, Project>().ForMember(p => p.ProjectId, opt => opt.MapFrom(g => Guid.NewGuid()));
        }
    }
}