using PostMortem.Domain.Questions;

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
            //Guard.IsNotNull(destination, nameof(destination)); // this breaks Upsert
            Guard.IsNotNull(context, nameof(context));

            destination = new DomainQuestion()
            {
                Importance = source.Importance,
                ProjectId = source.ProjectId,
                QuestionId = source.QuestionId,
                QuestionText = source.QuestionText,
                ResponseCount = source.ResponseCount
            };
            return destination;
        }
    }
}