namespace PostMortem.Web.Converters
{
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Domain.Questions;
    using PostMortem.Dtos;
    using Zatoichi.Common.Infrastructure.Extensions;

    public class QuestionConverter : ITypeConverter<Question, QuestionDto>
    {
        public QuestionDto Convert(Question source, QuestionDto destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            Guard.IsNotNull(destination, nameof(destination));
            Guard.IsNotNull(context, nameof(context));
            destination.QuestionId = source.QuestionId;
            destination.QuestionText = source.QuestionText;
            destination.ResponseCount = source.ResponseCount;
            source.Comments.Each(c => destination.Comments.Add(context.Mapper.Map<CommentDto>(c)));
            return destination;
        }
    }
}