namespace PostMortem.Domain.Projects
{
    using System;

    public class ProjectId : IProjectId
    {
        public ProjectId(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}