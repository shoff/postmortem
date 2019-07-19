
using Newtonsoft.Json;
using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Data.NEventStore
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using global::NEventStore;
    using ChaosMonkey.Guards;

    public partial class NEventStoreRepository
    {
        private readonly IStoreEvents eventStore;
        private readonly ILogger<NEventStoreRepository> logger;
        private static readonly JsonSerializerSettings SerialzerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public NEventStoreRepository(IStoreEvents eventStore, ILogger<NEventStoreRepository> logger)
        {
            this.logger = Guard.IsNotNull(logger, nameof(logger));
            this.eventStore = Guard.IsNotNull(eventStore, nameof(eventStore));
        }

        public Task SaveEventAsync(IEventArgs eventArgs)
        {
            return Task.Run(() =>
            {
                //using (var store = eventStore.OpenStream(GetBucketId(typeof(TAgg)), eventArgs.Id.AsIdString(), int.MinValue, int.MaxValue))
                //{
                //    store.Add(new EventMessage {Body = Serialize<T, TAgg>(eventArgs)});
                //    store.CommitChanges(Guid.NewGuid());
                //}
            });
        }

        private string GetBucketId(Type type)
        {
            return type.ToString();
        }

        string Serialize(IEventArgs eventArgs)
        {
            return JsonConvert.SerializeObject(eventArgs, Formatting.None,SerialzerSettings);
        }

        IEventArgs Deserialize(string body)
        {
            return JsonConvert.DeserializeObject(body,SerialzerSettings) as IEventArgs;
        }

        public IEnumerable<IEventArgs> GetAllEvents<TId, TAgg>(TId id)
            where TId : IEntityId
        {
            var commits = eventStore.Advanced.GetFrom(GetBucketId(typeof(TAgg)), id.AsIdString(),int.MinValue,int.MaxValue);
            foreach (var commit in commits)
            {
                foreach (var e in commit.Events)
                {
                    yield return Deserialize((string) e.Body);
                }
            }
        }
    }
}