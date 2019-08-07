namespace PostMortem.Data.MongoDb.Converters
{
    using System;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing;

    public class ESEventConverter : ITypeConverter<DomainEvent, ESEvent>
    {
        public ESEvent Convert(DomainEvent source, ESEvent destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            destination = new ESEvent
            {
                Body = JsonConvert.SerializeObject(source),
                EventType = Type.GetType(source.EventType),
                CommitDate = source.CommitDate
            };
            return destination;
        }
    }
}