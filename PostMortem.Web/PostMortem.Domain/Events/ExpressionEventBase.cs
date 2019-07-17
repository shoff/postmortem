namespace PostMortem.Domain.Events
{
    using System;

    public abstract class ExpressionEventBase<T> : EventArgs, IEvent<T>
    {
        public abstract string Expression { get; protected set; }
        public IEventId Id { get; protected set; }
        public virtual DateTime CommitDate { get; protected set; } = DateTime.UtcNow;
        public virtual DateTime LastUpdate { get; protected set; } = DateTime.UtcNow;
        public abstract T Apply(T t);
    
    }
}