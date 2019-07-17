namespace PostMortem.Domain.Events.Projects
{
    using System.Collections.Generic;
    using Domain.Projects;
    using MediatR;
    using Polly;

    public class ProjectGetAllEventArgs : Command<>, IRequest<PolicyResult<ICollection<Project>>>
    {
        public override T Apply<T>()
        {
            // NFI yet
            throw new System.NotImplementedException();
        }
    }
}