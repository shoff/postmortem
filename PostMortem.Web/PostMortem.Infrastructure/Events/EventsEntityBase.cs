using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PostMortem.Infrastructure.Events
{
    public abstract class EventsEntityBase<TEntityId, TEventArgs> : IEventsEntity<TEntityId,TEventArgs>
        where TEntityId : IEntityId
        where TEventArgs : IEventArgs
    {
        public abstract TEntityId GetEntityId();
        private List<TEventArgs> events=new List<TEventArgs>();

        protected void AppendEvent(TEventArgs @event) => events.Add(@event);
        public virtual IEnumerable<TEventArgs> GetPendingEvents() =>  new ReadOnlyCollection<TEventArgs>(events);
        public void ClearPendingEvents() => events.Clear();
        public abstract void ReplayEvent(TEventArgs eventArgs);
    }
}