namespace PostMortem.Infrastructure.Events.Projects
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using Domain.Projects.Events;
    using MediatR;
    using Polly;
    using Zatoichi.Common.Infrastructure.Resilience;
    using Project = Domain.Projects.Project;

    public class ProjectsGetAllRequestedHandler : IRequestHandler<ProjectGetAllEventArgs, PolicyResult<ICollection<Project>>>
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

        public Task<PolicyResult<ICollection<Project>>> Handle(ProjectGetAllEventArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.GetAllProjectsAsync());
        }
    }
}