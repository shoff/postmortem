namespace PostMortem.Web.Converters
{
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Domain.Questions;
    using PostMortem.Dtos;
    using Zatoichi.Common.Infrastructure.Extensions;

    public class QuestionDtoConverter : ITypeConverter<Question, QuestionDto>
    {
        public QuestionDto Convert(Question source, QuestionDto destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            Guard.IsNotNull(context, nameof(context));
            destination = new QuestionDto
            {
                QuestionId = source.QuestionId.Id,
                QuestionText = source.QuestionText,
                ResponseCount = source.ResponseCount,
                CommitDate = source.LastUpdated,
                Author = source.Author,
                Importance = source.Importance,
                ProjectId = source.ProjectId
            };
            source.Comments.Each(c => destination.Comments.Add(context.Mapper.Map<CommentDto>(c)));
            return destination;
        }
    }
}