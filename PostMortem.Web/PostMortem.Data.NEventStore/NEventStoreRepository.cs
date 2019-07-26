
using Newtonsoft.Json;
using PostMortem.Infrastructure.Events;

namespace PostMortem.Data.NEventStore
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::NEventStore;
    using ChaosMonkey.Guards;
    using PostMortem.Infrastructure;

    public abstract class NEventStoreRepository<TEntity,TEntityId,TEventArgs> : IEventStoreRepository<TEntity,TEntityId,TEventArgs> 
        where TEventArgs : class,IEventArgs
        where TEntity : IEventsEntity<TEntityId,TEventArgs>,new()
        where TEntityId : IEntityId
    {
        private readonly IStoreEvents eventStore;
        private readonly ILogger<NEventStoreRepository<TEntity,TEntityId,TEventArgs>> logger;
        private static readonly JsonSerializerSettings SerialzerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public NEventStoreRepository(IStoreEvents eventStore, ILogger<NEventStoreRepository<TEntity,TEntityId,TEventArgs>> logger)
        {
            this.logger = Guard.IsNotNull(logger, nameof(logger));
            this.eventStore = Guard.IsNotNull(eventStore, nameof(eventStore));
        }

        public Task SaveAsync(TEntity entity)
        {
            return Task.Run(() =>
            {
                using (var eventStream = eventStore.OpenStream(BucketName, GetStreamName(entity), int.MinValue, int.MaxValue))
                {
                    foreach (var eventArgs in entity.GetPendingEvents())
                    {
                        eventStream.Add(new EventMessage { Body = Serialize(eventArgs) });
                    }

                    eventStream.CommitChanges(Guid.NewGuid());
                    entity.ClearPendingEvents();
                }
            });
        }
        protected virtual string GetStreamName(TEntity entity) => entity.GetEntityId().AsIdString();

        protected virtual string BucketName => typeof(TEntity).ToString();

        string Serialize(TEventArgs eventArgs)
        {
            return JsonConvert.SerializeObject(eventArgs, Formatting.None,SerialzerSettings);
        }

        TEventArgs Deserialize(string body)
        {
            return JsonConvert.DeserializeObject(body,SerialzerSettings) as TEventArgs;
        }

        public IEnumerable<TEventArgs> LoadEvents(TEntityId id)
        {
            var commits = eventStore.Advanced.GetFrom(BucketName, id.AsIdString(),int.MinValue,int.MaxValue);
            foreach (var commit in commits)
            {
                foreach (var e in commit.Events)
                {
                    yield return Deserialize((string) e.Body);
                }
            }
        }

        public TEntity GetById(TEntityId id)
        {
            var entity=new TEntity();
            var events = LoadEvents(id);
            entity.ReplayEvents(events);
            return entity;
        }

        public Task DeleteByIdAsync(TEntityId id)
        {
            return Task.Run(() =>
            {
                eventStore.Advanced.DeleteStream(BucketName, id.AsIdString());
            });
        }
    }
}