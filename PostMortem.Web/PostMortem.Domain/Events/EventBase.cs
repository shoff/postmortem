namespace PostMortem.Domain.Events
{
    using System;

    public abstract class EventBase : EventArgs, IEvent
    {
        public virtual IEventId Id { get; protected set; }
        public virtual DateTime CommitDate { get; protected set; }
        public virtual DateTime LastUpdate { get; protected set; }
    }

    public abstract class EventBase<T> : EventBase, IEvent<T>
    {
        public abstract T Apply(T t);
        public abstract T Undo(T t);

    }
}