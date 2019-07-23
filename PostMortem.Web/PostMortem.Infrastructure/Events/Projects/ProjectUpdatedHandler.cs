using PostMortem.Data.MongoDb;
using PostMortem.Domain.Projects;

namespace PostMortem.Infrastructure.Events.Projects
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Domain;
    using Domain.Events.Projects;
    using MediatR;
    using Polly;
    using Zatoichi.Common.Infrastructure.Resilience;

    public class ProjectUpdatedHandler : IRequestHandler<ProjectUpdatedEventArgs, PolicyResult>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IProjectRepository repository;

        public ProjectUpdatedHandler(
            IProjectRepository repository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task<PolicyResult> Handle(ProjectUpdatedEventArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAndCaptureAsync(() => this.repository.SaveAsync(request.Project));
        }
    }
}