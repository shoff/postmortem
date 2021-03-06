﻿using PostMortem.Domain.Questions;

namespace PostMortem.Data.MongoDb.Converters
{
    using AutoMapper;
    using ChaosMonkey.Guards;
    using DomainQuestion = Domain.Questions.Question;

    public class QuestionConverter : ITypeConverter<DomainQuestion, Question>
    {
        public Question Convert(DomainQuestion source, Question destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            //Guard.IsNotNull(destination, nameof(destination)); // this breaks upserts
            Guard.IsNotNull(context, nameof(context));
            destination = new Question()
            {
                Importance = 0,
                ProjectId = source.ProjectId,
                QuestionId = source.QuestionId,
                QuestionText = source.QuestionText,
                ResponseCount = 0
            };
            return destination;
        }
    }
}