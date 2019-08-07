namespace PostMortem.Infrastructure.Projects
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Commands;
    using Domain;
    using Domain.Projects;
    using MediatR;
    using Zatoichi.Common.Infrastructure.Resilience;

    public class CreateProjectHandler : INotificationHandler<CreateProjectCommand>
    {
        private readonly IExecutionPolicies executionPolicies;
        private readonly IRepository repository;

        public CreateProjectHandler(
            IRepository repository,
            IExecutionPolicies executionPolicies)
        {
            this.executionPolicies = Guard.IsNotNull(executionPolicies, nameof(executionPolicies));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task Handle(CreateProjectCommand notification, CancellationToken cancellationToken)
        {
            var project = new Project(
                notification.ProjectName,
                notification.StartDate,
                notification.EndDate);

            return this.repository.CreateProjectAsync(project);
        }
    }
}