namespace PostMortem.Data.MongoDb.Converters
{
    using AutoMapper;
    using ChaosMonkey.Guards;
    using DomainQuestion = Domain.Questions.Question;

    public class DomainQuestionConverter : ITypeConverter<Question, DomainQuestion>
    {
        public DomainQuestion Convert(Question source, DomainQuestion destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            Guard.IsNotNull(destination, nameof(destination));
            Guard.IsNotNull(context, nameof(context));

            destination = new DomainQuestion()
            {
                Importance = 0,
                ProjectId = source.ProjectId,
                QuestionId = source.Id,
                QuestionText = source.QuestionText,
                ResponseCount = 0
            };
            return destination;
        }
    }
}