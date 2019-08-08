namespace PostMortem.Data.MongoDb.Converters
{
    using System;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Domain.Comments.Events;
    using Domain.Projects.Events;
    using Domain.Questions.Events;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing;

    public class EsEventConverter : ITypeConverter<DomainEvent, EsEvent>
    {
        // ReSharper disable once RedundantAssignment
        public EsEvent Convert(DomainEvent source, EsEvent destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            destination = new EsEvent
            {
                Body = JsonConvert.SerializeObject(source),
                EventType = Type.GetType(source.EventType),
                CommitDate = source.CommitDate
            };

            // TODO strategy to handle this

            if (source is CommentEvent commentEvent)
            {
                destination.CommentId = commentEvent.CommentId;
                destination.VoterId = commentEvent.VoterId;
            }

            if (source is QuestionEvent questionEvent)
            {
                destination.VoterId = questionEvent.Author;
                destination.ProjectId = questionEvent.ProjectId;
                destination.QuestionId = questionEvent.QuestionId;
            }

            if (source is ProjectEvent projectEvent)
            {
                destination.VoterId = projectEvent.Author;
                destination.ProjectId = projectEvent.ProjectId;
            }

            return destination;
        }
    }
}