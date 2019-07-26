using System.Collections.Generic;

namespace PostMortem.Infrastructure.Events
{
    public interface IEventsEntity<TEntityId, TEventArgs> : IEntity<TEntityId>
        where TEntityId : IEntityId
        where TEventArgs : IEventArgs
    {
        IEnumerable<TEventArgs> GetPendingEvents();
        void ClearPendingEvents();
        void ReplayEvent(TEventArgs eventArgs);
    }
}