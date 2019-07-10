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

    public class ProjectCreatedHandler : IRequestHandler<ProjectCreatedEventArgs, PolicyResult>
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

        public Task<PolicyResult> Handle(ProjectCreatedEventArgs request, CancellationToken cancellationToken)
        {
            return this.executionPolicies.DbExecutionPolicy.ExecuteAsync(() => this.repository.CreateProjectAsync(request.Project));
        }
    }
}