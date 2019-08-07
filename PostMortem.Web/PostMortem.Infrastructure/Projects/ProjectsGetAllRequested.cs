namespace PostMortem.Infrastructure.Projects
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using MediatR;
    using Polly;
    using Queries;
    using Zatoichi.Common.Infrastructure.Resilience;
    using Project = Domain.Projects.Project;

    public class ProjectsGetAllRequestedHandler : IRequestHandler<ProjectGetAllEvent, PolicyResult<ICollection<Project>>>
    { 
    private readonly IExecutionPolicies executionPolicies;
        private readonly IRepository repository;

        public ProjectsGetAllRequestedHandler(
            IRepository repository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task<PolicyResult<ICollection<Project>>> Handle(ProjectGetAllEvent request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.GetAllProjectsAsync());
        }
    }
}