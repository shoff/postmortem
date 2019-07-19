namespace PostMortem.Domain.Events
{
    using System;
    using System.Linq.Expressions;

    public abstract class Command<T> : EventArgs, IEvent<T>
    {
        public abstract T Apply(T t);
        
        public virtual IAggregateId Id { get; protected set; }
        public virtual DateTime CommitDate { get; protected set; } = DateTime.UtcNow;
        public virtual DateTime LastUpdate { get; protected set; } = DateTime.UtcNow;
    }
}