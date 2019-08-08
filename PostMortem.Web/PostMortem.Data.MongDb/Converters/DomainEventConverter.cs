namespace PostMortem.Data.MongoDb.Converters
{
    using System;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Domain.Comments.Events;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing;

    public class DomainEventConverter : ITypeConverter<EsEvent, DomainEvent>
    {

        // ReSharper disable once RedundantAssignment
        public DomainEvent Convert(EsEvent source, DomainEvent destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));

            if (source.EventType == typeof(CommentAdded).FullName)
            {
               var @event = JsonConvert.DeserializeObject<CommentAdded>(source.Body);
               destination = @event;
               //return destination;
            }
            // object o = JsonConvert.DeserializeObject(source.Body, Type.GetType(source.EventType));

            return destination;
        }
    }
}