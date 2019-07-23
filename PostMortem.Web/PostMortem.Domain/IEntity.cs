using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Domain
{
    public interface IEntity<TEntityId>
        where TEntityId : IEntityId
    {
        TEntityId GetEntityId();
    }

    public interface IEventsEntity<TEntityId, TEventArgs> : IEntity<TEntityId>
        where TEntityId : IEntityId
        where TEventArgs : IEventArgs
    {
        IEnumerable<TEventArgs> GetPendingEvents();
        void ClearPendingEvents();
        void ReplayEvent(TEventArgs eventArgs);
    }

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
