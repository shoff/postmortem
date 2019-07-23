using PostMortem.Data.MongoDb;
using PostMortem.Domain.Projects;

namespace PostMortem.Infrastructure.Events.Projects
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using Domain.Events.Projects;
    using MediatR;
    using Polly;
    using Zatoichi.Common.Infrastructure.Resilience;
    using Project = Domain.Projects.Project;

    public class ProjectsGetAllRequestedHandler : IRequestHandler<GetAllProjectsQueryArgs, PolicyResult<IEnumerable<Project>>>
    { 
    private readonly IExecutionPolicies executionPolicies;
        private readonly IProjectRepository repository;

        public ProjectsGetAllRequestedHandler(
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
    }
}