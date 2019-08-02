namespace PostMortem.Domain.Projects.Events
{
    using System.Collections.Generic;
    using MediatR;
    using Polly;
    using Projects;

    public class ProjectGetAllEventArgs : IRequest<PolicyResult<ICollection<Project>>>
    {
    }
}