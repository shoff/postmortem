using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using NEventStore;
using NEventStore.Logging;
using NEventStore.Persistence;
using NEventStore.Serialization;
using PostMortem.Data.NEventStore.Config;

namespace PostMortem.Data.NEventStore
{
    public class MongDbStoreEvents : IStoreEvents
    {
        private IStoreEvents internalEventStore;

        public MongDbStoreEvents(IOptions<MongoDbStoreEventsOptions> options)
        {
            internalEventStore = Initialize(options.Value);
        }

        private IStoreEvents Initialize(MongoDbStoreEventsOptions options)
        {
            return Wireup.Init()
                .LogToOutputWindow(LogLevel.Verbose)
                .LogToConsoleWindow(LogLevel.Verbose)
                .UseOptimisticPipelineHook()
                .UsingMongoPersistence(BuildConnectionString(options), new DocumentObjectSerializer())
                .InitializeStorageEngine()
                //.HookIntoPipelineUsing(new[] { new AuthorizationPipelineHook() }) // hook up a custom hook to authorize viewing of specific commits
                .Build();
        }

        private string BuildConnectionString(MongoDbStoreEventsOptions options)
        {
            // from MongoDb's documentation the connections string
            // mongodb://[username:password@]host1[:port1][,...hostN[:portN]][/[database][?options]]
            return $"mongodb://{options.Host}:{options.Port}/{options.DefaultDb}";
        }

        public void Dispose()
        {
            internalEventStore.Dispose();
        }

        public IEventStream CreateStream(string bucketId, string streamId)
        {
            return internalEventStore.CreateStream(bucketId, streamId);
        }

        public IEventStream OpenStream(string bucketId, string streamId, int minRevision, int maxRevision)
        {
            return internalEventStore.OpenStream(bucketId, streamId, minRevision, maxRevision);
        }

        public IEventStream OpenStream(ISnapshot snapshot, int maxRevision)
        {
            return internalEventStore.OpenStream(snapshot, maxRevision);
        }
        public IPersistStreams Advanced => internalEventStore.Advanced; 
        
    }
}


