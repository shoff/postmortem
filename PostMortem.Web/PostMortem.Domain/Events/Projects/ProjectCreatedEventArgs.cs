﻿namespace PostMortem.Domain.Events.Projects
{
    using ChaosMonkey.Guards;
    using Domain.Projects;
    using MediatR;
    using Polly;

    public class ProjectCreatedEventArgs : IRequest<PolicyResult>
    {
        public ProjectCreatedEventArgs(Project project)
        {
            this.Project = Guard.IsNotNull(project, nameof(project));
        }

        public Project Project { get; private set; }
    }
}