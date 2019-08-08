namespace PostMortem.Data.MongoDb.Converters
{
    using System;
    using System.Linq;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Zatoichi.Common.Infrastructure.Extensions;
    using DomainQuestion = Domain.Questions.Question;

    public class QuestionConverter : ITypeConverter<DomainQuestion, Question>
    {
        public Question Convert(DomainQuestion source, Question destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            destination = new Question()
            {
                Importance = 0,
                ProjectId = source.ProjectId,
                Id = source.QuestionId.Id,
                QuestionText = source.QuestionText,
                ResponseCount = 0,
                Author = source.Author,
                CommitDate = DateTime.UtcNow,
                Comments = source.Comments.Map(c => context.Mapper.Map<Comment>(c)).ToList()
            };
            return destination;
        }
    }

}