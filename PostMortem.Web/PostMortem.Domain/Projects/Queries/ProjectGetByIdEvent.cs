namespace PostMortem.Domain.Projects.Queries
{
    using System;
    using ChaosMonkey.Guards;
    using MediatR;
    using Polly;
    using Projects;

    public class ProjectGetByIdEvent : IRequest<PolicyResult<Project>>
    {
        public ProjectGetByIdEvent(Guid projectId)
        {
            this.ProjectId = Guard.IsNotDefault(projectId, nameof(projectId));
        }

        public Guid ProjectId { get; private set; }
    }
}