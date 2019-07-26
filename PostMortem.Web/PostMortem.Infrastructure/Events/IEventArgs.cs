using MediatR;

namespace PostMortem.Infrastructure.Events
{
    public interface IEventArgs : INotification
    {

        //bool IsReplaying { get; set; }
        IEntityId GetEntityId();
    }
}
