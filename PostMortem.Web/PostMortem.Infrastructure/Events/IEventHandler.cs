using MediatR;

namespace PostMortem.Infrastructure.Events
{
    public interface IEventHandler<in TEventArgs> : INotificationHandler<TEventArgs>
        where TEventArgs : IEventArgs
    {
        
    }
}