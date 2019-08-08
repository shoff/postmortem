namespace PostMortem.Data.MongoDb.Converters
{
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Newtonsoft.Json;
    using Zatoichi.EventSourcing;

    public class DomainEventConverter : ITypeConverter<EsEvent, DomainEvent>
    {

        // ReSharper disable once RedundantAssignment
        public DomainEvent Convert(EsEvent source, DomainEvent destination, ResolutionContext context)
        {
            Guard.IsNotNull(source, nameof(source));
            destination = (DomainEvent) JsonConvert.DeserializeObject(source.Body);
            return destination;
        }
    }
}