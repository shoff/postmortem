﻿namespace PostMortem.Domain.Events.Projects
{
    using System;
    using ChaosMonkey.Guards;
    using Domain.Projects;
    using MediatR;
    using Polly;

    public class ProjectCreatedEventArgs : EventBase<Project>, IRequest<PolicyResult<Guid>>
    {
        public ProjectCreatedEventArgs(Project project)
        {
            this.Project = Guard.IsNotNull(project, nameof(project));
        }

        public Project Project { get; private set; }

        public override Project Apply(Project t)
        {
            throw new NotImplementedException();
        }

        public override Project Undo(Project t)
        {
            throw new NotImplementedException();
        }
    }
}