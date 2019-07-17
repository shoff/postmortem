namespace PostMortem.Infrastructure.Events.Projects
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Events.Projects;
    using MediatR;
    using Polly;

    public class ProjectGetByIdHandler : IRequestHandler<ProjectCreatedEventArgs, PolicyResult<Guid>>
    {
        public Task<PolicyResult<Guid>> Handle(ProjectCreatedEventArgs request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}