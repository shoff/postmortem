using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PostMortem.Infrastructure.Events
{
    public abstract class EventsEntityBase<TEntityId, TEventArgs> : IEventsEntity<TEntityId,TEventArgs>
        where TEntityId : IEntityId
        where TEventArgs : IEventArgs
    {
        public EventsEntityBase(){ }
        protected EventsEntityBase(IEnumerable<TEventArgs> buildFromEvents)
        {
            ReplayEvents(buildFromEvents);
        }

        public abstract TEntityId GetEntityId();
        private readonly List<TEventArgs> events=new List<TEventArgs>();

        protected virtual void AppendEvent(TEventArgs @event) => events.Add(@event);
        public virtual IEnumerable<TEventArgs> GetPendingEvents() =>  new ReadOnlyCollection<TEventArgs>(events);
        public virtual void ClearPendingEvents() => events.Clear();
        public abstract void ReplayEvent(TEventArgs eventArgs);

        public void ReplayEvents(IEnumerable<TEventArgs> eventArgs)
        {
            foreach (var eventArg in eventArgs)
            {
                    ReplayEvent(eventArg);
            }
        }
    }
}