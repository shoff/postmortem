namespace PostMortem.Domain.Projects.Commands
{
    using System;
    using ChaosMonkey.Guards;
    using MediatR;
    using Polly;
    using Projects;

    public class ProjectCreatedEvent : IRequest<PolicyResult<Guid>>
    {
        public ProjectCreatedEvent(Project project)
        {
            this.Project = Guard.IsNotNull(project, nameof(project));
        }

        public Project Project { get; private set; }
    }
}