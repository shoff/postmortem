using MediatR;

namespace PostMortem.Domain.EventSourcing.Events
{
    public interface IEventArgs : INotification
    {
        bool IsReplaying { get; set; }
        IEntityId GetEntityId();
    }
}
