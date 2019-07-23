using MediatR;
using Newtonsoft.Json;

namespace PostMortem.Domain.EventSourcing.Events
{
    public interface IEventArgs //: INotification
    {
        [JsonIgnore]
        bool IsReplaying { get; set; }
        IEntityId GetEntityId();
    }
}
