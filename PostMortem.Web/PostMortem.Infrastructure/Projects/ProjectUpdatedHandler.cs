namespace PostMortem.Infrastructure.Projects
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Commands;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Zatoichi.Common.Infrastructure.Resilience;

    public class ProjectUpdatedHandler : INotificationHandler<UpdateProjectCommand>
    {
        private readonly ILogger<ProjectUpdatedHandler> logger;
        private readonly IRepository repository;

        public ProjectUpdatedHandler(
            ILogger<ProjectUpdatedHandler> logger,
            IRepository repository)
        {
            this.logger = Guard.IsNotNull(logger, nameof(logger));
            this.repository = Guard.IsNotNull(repository, nameof(repository));
        }

        public Task Handle(UpdateProjectCommand notification, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(notification, nameof(notification));
            throw new NotImplementedException();
        }
    }
}