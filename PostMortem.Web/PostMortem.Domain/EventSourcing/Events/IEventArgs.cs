using MediatR;
using Newtonsoft.Json;
using Polly;

namespace PostMortem.Domain.EventSourcing.Events
{
    public interface IEventArgs : IRequest<PolicyResult> // should this be INotification<PolicyResult>?
    {
        [JsonIgnore]
        bool IsReplaying { get; set; }
        IEntityId GetEntityId();
    }
}
