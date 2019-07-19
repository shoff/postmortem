
using System.Runtime.CompilerServices;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using PostMortem.Domain.Events;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace PostMortem.Data.NEventStore
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Comments;
    using Domain.Projects;
    using Domain.Questions;
    using Polly;
    using global::NEventStore;
    using ChaosMonkey.Guards;
    using DomainProject = Domain.Projects.Project;
    using DomainQuestion = Domain.Questions.Question;
    using DomainComment = Domain.Comments.Comment;

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

        public Task SaveEventAsync<T, TAgg>(T eventArgs)
            where T : IEvent<TAgg>
        {
            return Task.Run(() =>
            {
                using (var store = eventStore.OpenStream(GetBucketId(typeof(TAgg)), eventArgs.Id.AsIdString(), int.MinValue, int.MaxValue))
                {
                    store.Add(new EventMessage {Body = Serialize<T, TAgg>(eventArgs)});
                    store.CommitChanges(Guid.NewGuid());
                }
            });
        }

        private string GetBucketId(Type type)
        {
            return type.ToString();
        }

        string Serialize<T, TAgg>(T eventArgs)
            where T : IEvent<TAgg>
        {
            return JsonConvert.SerializeObject(eventArgs, Formatting.None,SerialzerSettings);
        }

        T Deserialize<T, TAgg>(string body)
            where T : class, IEvent<TAgg>
        {
            return JsonConvert.DeserializeObject<T>(body,SerialzerSettings);
        }

        public IEnumerable<IEvent<TAgg>> GetAllEvents<T, TId, TAgg>(TId id)
            where T : class, IEvent<TAgg>
            where TId : IAggregateId
        {
            //TODO: figure out how to resolve bucket and stream ID
            var commits = eventStore.Advanced.GetFrom(GetBucketId(typeof(TAgg)), id.AsIdString(),int.MinValue,int.MaxValue);
            foreach (var commit in commits)
            {
                foreach (var e in commit.Events)
                {
                    yield return Deserialize<T, TAgg>((string) e.Body);
                }
            }
        }
    }
}