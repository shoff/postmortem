namespace PostMortem.Domain.Events
{
    using System;

    public abstract class EventBase<T> : EventArgs, IEvent<T>
    {
        public abstract T Apply(T t);
        public abstract T Undo(T t);
        public virtual IEventId Id { get; protected set; }
        public virtual DateTime CommitDate { get; protected set; }
        public virtual DateTime LastUpdate { get; protected set; }
    }
}