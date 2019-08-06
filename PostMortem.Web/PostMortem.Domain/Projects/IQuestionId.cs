namespace PostMortem.Domain.Projects
{
    using System;

    public interface IProjectId
    {
        Guid Id { get; }
    }
}