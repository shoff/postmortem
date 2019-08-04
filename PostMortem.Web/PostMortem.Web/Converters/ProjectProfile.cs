namespace PostMortem.Web.Converters
{
    using System;
    using AutoMapper;
    using Domain.Comments;
    using Domain.Questions;
    using PostMortem.Dtos;
    using Project = Domain.Projects.Project;

    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            this.CreateMap<Comment, CommentDto>().ConvertUsing<CommentDtoConverter>();
            this.CreateMap<Question, QuestionDto>().ConvertUsing<QuestionConverter>();
            this.CreateMap<Data.MongoDb.Project, Project>();
            this.CreateMap<CreateProjectDto, Project>().ForMember(p => p.ProjectId, opt => opt.MapFrom(g => Guid.NewGuid()));
            this.CreateMap<Project, ProjectDto>().ConvertUsing<ProjectDtoConverter>();
        }
    }
}