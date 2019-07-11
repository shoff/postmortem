namespace PostMortem.Data.MongoDb.Converters
{
    using AutoMapper;
    using DomainProject = Domain.Projects.Project;
    using DomainComment = Domain.Comments.Comment;
    using DomainQuestion = Domain.Questions.Question;

    public class MongoDbProfile : Profile
    {
        public MongoDbProfile()
        {
            this.CreateMap<DomainProject, Project>().ConvertUsing<ProjectConverter>();
            this.CreateMap<DomainComment, Comment>().ConvertUsing<CommentConverter>();
            this.CreateMap<DomainQuestion, Question>().ConvertUsing<QuestionConverter>();
        }
    }
}