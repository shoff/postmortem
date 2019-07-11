namespace PostMortem.Domain.Events.Projects
{
    using System.Collections.Generic;
    using Domain.Projects;
    using MediatR;
    using Polly;

    public class ProjectGetAllEventArgs : IRequest<PolicyResult<ICollection<Project>>>
    {
    }
}