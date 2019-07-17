namespace PostMortem.Domain.Events
{
    using System;

    public interface IEvent<T>
    {
        T Apply(T t);
        IEventId Id { get; }
        DateTime CommitDate { get; }
        DateTime LastUpdate { get; }
    }
}