using System.Collections.Generic;
using PostMortem.Domain.EventSourcing.Queries;
using PostMortem.Domain.Projects;

namespace PostMortem.Infrastructure.Events.Projects
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using MediatR;
    using Polly;
    using Zatoichi.Common.Infrastructure.Resilience;

    public class ProjectQueryHandler : 
        IQueryHandler<GetAllProjectsQueryArgs,IEnumerable<Project>>,
        IQueryHandler<GetProjectByIdQueryArgs,Project>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IProjectRepository repository;

        public ProjectQueryHandler(
            IProjectRepository repository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task<PolicyResult<IEnumerable<Project>>> Handle(GetAllProjectsQueryArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.GetAllAsync());
        }

        public Task<PolicyResult<Project>> Handle(GetProjectByIdQueryArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.GetByIdAsync(request.ProjectId));
        }
    }
}