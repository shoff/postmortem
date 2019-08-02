﻿namespace PostMortem.Infrastructure.Events.Projects
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using Domain.Projects.Events;
    using MediatR;
    using Polly;
    using Zatoichi.Common.Infrastructure.Resilience;

    public class ProjectCreatedHandler : IRequestHandler<ProjectCreatedEventArgs, PolicyResult<Guid>>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IRepository repository;

        public ProjectCreatedHandler(
            IRepository repository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task<PolicyResult<Guid>> Handle(ProjectCreatedEventArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.CreateProjectAsync(request.Project));
        }
    }
}