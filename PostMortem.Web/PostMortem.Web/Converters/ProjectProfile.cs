namespace PostMortem.Web.Converters
{
    using System;
    using AutoMapper;
    using Domain.Comments;
    using Domain.Questions;
    using Dtos;
    using Project = Domain.Projects.Project;

    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            this.CreateMap<Comment, CommentDto>().ConvertUsing<CommentConverter>();
            this.CreateMap<Question, QuestionDto>().ConvertUsing<QuestionConverter>();
            this.CreateMap<Data.MongoDb.Project, Project>();
            this.CreateMap<CreateProjectDto, Data.MongoDb.Project>().ForMember(p => p.ProjectId, opt => opt.MapFrom(g => Guid.NewGuid()));
        }
    }
}