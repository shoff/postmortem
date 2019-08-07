namespace PostMortem.Infrastructure.Projects.Queries
{
    using System.Collections.Generic;
    using Domain.Projects;
    using MediatR;
    using Polly;

    public class ProjectGetAllEvent : IRequest<PolicyResult<ICollection<Project>>>
    {
    }
}