namespace PostMortem.Domain.Events
{
    using System;

    public interface IEntityId
    {
        Guid Id { get; }
    }
}