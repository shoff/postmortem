using System;
using ChaosMonkey.Guards;
using Polly;
using PostMortem.Infrastructure.Queries;

namespace PostMortem.Domain.Projects
{
    public class GetProjectByIdQueryArgs : QueryArgsBase<Project>
    {
        public GetProjectByIdQueryArgs(Guid projectId)
        {
            this.ProjectId = Guard.IsNotDefault(projectId, nameof(projectId));
        }

        public Guid ProjectId { get; private set; }
    }
}