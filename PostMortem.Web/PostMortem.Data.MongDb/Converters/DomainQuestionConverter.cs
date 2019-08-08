namespace PostMortem.Data.MongoDb.Converters
{
    using AutoMapper;
    using ChaosMonkey.Guards;

    public class DomainQuestionConverter : ITypeConverter<Question, Domain.Questions.Question>
    {
        public Domain.Questions.Question Convert(Question source, Domain.Questions.Question destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            destination =
                new Domain.Questions.Question(source.QuestionText, source.ProjectId, source.Author, source.QuestionId);
            return destination;
        }
    }
}