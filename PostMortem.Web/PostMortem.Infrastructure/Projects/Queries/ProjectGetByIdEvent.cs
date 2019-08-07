namespace PostMortem.Infrastructure.Projects.Queries
{
    using System;
    using ChaosMonkey.Guards;
    using Domain.Projects;
    using MediatR;
    using Polly;

    public class ProjectGetByIdEvent : IRequest<PolicyResult<Project>>
    {
        public ProjectGetByIdEvent(Guid projectId)
        {
            this.ProjectId = Guard.IsNotDefault(projectId, nameof(projectId));
        }

        public Guid ProjectId { get; private set; }
    }
}