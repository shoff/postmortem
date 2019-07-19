using MediatR;

namespace PostMortem.Domain.EventSourcing.Events
{
    public interface IEventHandler<in TArgs> : INotificationHandler<TArgs>
        where TArgs : IEventArgs
    {

    }
}