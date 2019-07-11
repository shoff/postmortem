namespace PostMortem.Domain.Events.Projects
{
    using System;
    using ChaosMonkey.Guards;
    using Domain.Projects;
    using MediatR;
    using Polly;

    public class ProjectGetByIdEventArgs : IRequest<PolicyResult<Project>>
    {
        public ProjectGetByIdEventArgs(Guid projectId)
        {
            this.ProjectId = Guard.IsNotDefault(projectId, nameof(projectId));
        }

        public Guid ProjectId { get; private set; }
    }
}