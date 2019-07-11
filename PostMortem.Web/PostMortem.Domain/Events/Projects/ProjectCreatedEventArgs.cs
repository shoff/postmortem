namespace PostMortem.Domain.Events.Projects
{
    using System;
    using ChaosMonkey.Guards;
    using Domain.Projects;
    using MediatR;
    using Polly;

    public class ProjectCreatedEventArgs : IRequest<PolicyResult<Guid>>
    {
        public ProjectCreatedEventArgs(Project project)
        {
            this.Project = Guard.IsNotNull(project, nameof(project));
        }

        public Project Project { get; private set; }
    }
}