namespace PostMortem.Data.EventSourcing
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using ChaosMonkey.Guards;
    using Config;
    using Domain.Events;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public class EventStore
    {
        private readonly IMapper mapper;
        private readonly ILogger<EventStore> logger;
        private readonly IMongoDatabase database;

        public EventStore(IMapper mapper,
            ILogger<EventStore> logger,
            IOptions<MongoOptions> options)
        {
            Guard.IsNotNull(options, nameof(options));
            this.mapper = Guard.IsNotNull(mapper, nameof(mapper));
            this.logger = Guard.IsNotNull(logger, nameof(logger));

            MongoInternalIdentity internalIdentity = new MongoInternalIdentity(Constants.ADMIN, options.Value.Username);
            PasswordEvidence passwordEvidence = new PasswordEvidence(options.Value.Password);
            MongoCredential mongoCredential = new MongoCredential(options.Value.AuthMechanism, internalIdentity, passwordEvidence);

            MongoClientSettings settings = new MongoClientSettings
            {
                Credential = mongoCredential,
                Server = new MongoServerAddress(options.Value.MongoHost, int.Parse(options.Value.Port))
            };

            var client = new MongoClient(settings);
            this.database = client.GetDatabase(options.Value.DefaultDb);
        }

        public async Task<Guid> InsertEvent<T>(IEvent<T> @event)
        {

        }


    }
}