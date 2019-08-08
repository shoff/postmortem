namespace PostMortem.Data.MongoDb.Converters
{
    using AutoMapper;
    using Zatoichi.EventSourcing;

    public class MongoDbProfile : Profile
    {
        public MongoDbProfile()
        {
            this.CreateMap<Domain.Projects.Project, Project>().ConvertUsing<ProjectConverter>();
            this.CreateMap<Project, Domain.Projects.Project>().ConvertUsing<DomainProjectConverter>();
            this.CreateMap<Domain.Comments.Comment, Comment>().ConvertUsing<CommentConverter>();
            this.CreateMap<Domain.Questions.Question, Question>().ConvertUsing<QuestionConverter>();
            this.CreateMap<DomainEvent, EsEvent>().ConvertUsing<EsEventConverter>();
        }
    }
}