namespace PostMortem.Domain.Projects.Events
{
    using System;
    using ChaosMonkey.Guards;
    using MediatR;
    using Polly;
    using Projects;

    public class ProjectCreatedEventArgs : IRequest<PolicyResult<Guid>>
    {
        public ProjectCreatedEventArgs(Project project)
        {
            this.Project = Guard.IsNotNull(project, nameof(project));
        }

        public Project Project { get; private set; }
    }
}