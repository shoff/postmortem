using System.Linq;

namespace PostMortem.Web.Converters
{
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Domain.Questions;
    using Dtos;
    using Zatoichi.Common.Infrastructure.Extensions;

    public class QuestionConverter : ITypeConverter<Question, QuestionDto>
    {
        public QuestionDto Convert(Question source, QuestionDto destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            //Guard.IsNotNull(destination, nameof(destination)); //this breaks upsert
            Guard.IsNotNull(context, nameof(context));
            destination = new QuestionDto
            {
                Importance = source.Importance,
                QuestionId = source.QuestionId,
                QuestionText = source.QuestionText,
                ResponseCount = source.ResponseCount,
                Comments = source.Comments?.Select(c => context.Mapper.Map<CommentDto>(c)).ToList()

            };
            return destination;
        }
    }
}