using AutoMapper;
using PostMortem.Domain;

namespace PostMortem.Data.NEventStore
{
    using Config;
    using System;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain.Events;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class EventStore
    {
        private readonly IMapper mapper;
        private readonly ILogger<EventStore> logger;
        private readonly IRepository readModel;
        private readonly IEventBroker broker;

        public EventStore(
            IEventBroker broker,
            IMapper mapper,
            ILogger<EventStore> logger,
            IOptions<NEventStoreOptions> options,
            IRepository readModel
            )
        {
            Guard.IsNotNull(options, nameof(options));
            this.mapper = Guard.IsNotNull(mapper, nameof(mapper));
            this.logger = Guard.IsNotNull(logger, nameof(logger));
            this.readModel = Guard.IsNotNull(readModel,nameof(this.readModel));
            this.broker = Guard.IsNotNull(broker, nameof(broker));
            //MongoInternalIdentity internalIdentity = new MongoInternalIdentity(Constants.ADMIN, options.Value.Username);
            //PasswordEvidence passwordEvidence = new PasswordEvidence(options.Value.Password);
            //MongoCredential mongoCredential = new MongoCredential(options.Value.AuthMechanism, internalIdentity, passwordEvidence);

            //MongoClientSettings settings = new MongoClientSettings
            //{
            //Credential = mongoCredential,
            //Server = new MongoServerAddress(options.Value.MongoHost, int.Parse(options.Value.Port))
            //};

            //var client = new MongoClient(settings);
            //this.readModel = client.GetDatabase(options.Value.DefaultDb);
        }

        public async Task<Guid> InsertEvent<T>(IEvent<T> @event)
        {
            throw new NotImplementedException();
        }


    }
}