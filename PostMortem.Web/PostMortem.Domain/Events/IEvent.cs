namespace PostMortem.Domain.Events
{
    using System;

    public interface IEvent
    {
        IEventId Id { get; }
        DateTime CommitDate { get; }
        DateTime LastUpdate { get; }
    }

    public interface IEvent<T> : IEvent
    {
        T Apply(T t);
        T Undo(T t);

    }
}