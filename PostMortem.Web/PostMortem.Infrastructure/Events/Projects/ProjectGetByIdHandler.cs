namespace PostMortem.Infrastructure.Events.Projects
{
    using System;
    using Domain.Events.Projects;
    using MediatR;
    using Polly;

    public class ProjectGetByIdHandler : IRequestHandler<ProjectCreatedEventArgs, PolicyResult<Guid>>
    {
        
    }
}