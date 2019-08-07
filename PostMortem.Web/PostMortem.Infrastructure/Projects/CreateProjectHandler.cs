namespace PostMortem.Infrastructure.Projects
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Commands;
    using Domain.Projects;
    using MediatR;

    public class CreateProjectHandler : INotificationHandler<CreateProjectCommand>
    {
        private readonly IRepository repository;

        public CreateProjectHandler(
            IRepository repository)
        {
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task Handle(CreateProjectCommand notification, CancellationToken cancellationToken)
        {
            var project = new Project(
                notification.ProjectName,
                notification.CreatedBy,
                notification.StartDate,
                notification.EndDate);

            return this.repository.CreateProjectAsync(project);
        }
    }
}