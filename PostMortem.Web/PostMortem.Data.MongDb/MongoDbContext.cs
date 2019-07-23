using System;
using System.Collections.Generic;
using System.Text;
using ChaosMonkey.Guards;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PostMortem.Data.MongoDb.Config;

namespace PostMortem.Data.MongoDb
{
    public class MongoDbContext : IMongoDbContext
    {
        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }
        public MongoDbContext(ILogger<MongoDbContext> logger, IOptions<MongoOptions> options)
        {
            Guard.IsNotNull(options, nameof(options));
            Guard.IsNotNull(logger, nameof(logger));

            MongoInternalIdentity internalIdentity = new MongoInternalIdentity(Constants.ADMIN, options.Value.Username);
            PasswordEvidence passwordEvidence = new PasswordEvidence(options.Value.Password);
            MongoCredential mongoCredential = new MongoCredential(options.Value.AuthMechanism, internalIdentity, passwordEvidence);

            MongoClientSettings settings = new MongoClientSettings
            {
                //Credential = mongoCredential, -- no UN/PW on my box.
                Server = new MongoServerAddress(options.Value.MongoHost, int.Parse(options.Value.Port))
            };

            Client = new MongoClient(settings);
            Database = Client.GetDatabase(options.Value.DefaultDb);
        }
    }
}
