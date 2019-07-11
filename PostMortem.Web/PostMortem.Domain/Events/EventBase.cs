namespace PostMortem.Domain.Events
{
    using System;
    using System.Linq.Expressions;

    public abstract class EventBase : EventArgs, IEvent
    {
        public abstract T Apply<T>();
        public virtual IEventId Id { get; protected set; }
        public virtual DateTime CommitDate { get; protected set; } = DateTime.UtcNow;
        public virtual DateTime LastUpdate { get; protected set; } = DateTime.UtcNow;
    }

    public abstract class EventBase<T> : EventArgs, IEvent<T>
    {
        public abstract T Apply(T t);
        public abstract T Undo(T t);
        public virtual IEventId Id { get; protected set; }
        public virtual DateTime CommitDate { get; protected set; } = DateTime.UtcNow;
        public virtual DateTime LastUpdate { get; protected set; } = DateTime.UtcNow;
    }

    public abstract class ExpressionEventBase<T> : EventArgs, IEvent<T>
    {
        public abstract string Expression { get; protected set; }
        public IEventId Id { get; protected set; }
        public DateTime CommitDate { get; protected set; } = DateTime.UtcNow;
        public DateTime LastUpdate { get; protected set; } = DateTime.UtcNow;
        public abstract T Apply(T t);
        public abstract T Undo(T t);
    }
}