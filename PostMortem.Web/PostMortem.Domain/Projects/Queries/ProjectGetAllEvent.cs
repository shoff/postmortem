namespace PostMortem.Domain.Projects.Queries
{
    using System.Collections.Generic;
    using MediatR;
    using Polly;
    using Projects;

    public class ProjectGetAllEvent : IRequest<PolicyResult<ICollection<Project>>>
    {
    }
}