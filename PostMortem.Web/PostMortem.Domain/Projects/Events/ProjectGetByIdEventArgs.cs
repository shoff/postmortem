namespace PostMortem.Domain.Projects.Events
{
    using System;
    using ChaosMonkey.Guards;
    using MediatR;
    using Polly;
    using Projects;

    public class ProjectGetByIdEventArgs : IRequest<PolicyResult<Project>>
    {
        public ProjectGetByIdEventArgs(Guid projectId)
        {
            this.ProjectId = Guard.IsNotDefault(projectId, nameof(projectId));
        }

        public Guid ProjectId { get; private set; }
    }
}