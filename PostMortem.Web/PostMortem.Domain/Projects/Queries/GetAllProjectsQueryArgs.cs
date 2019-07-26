using System.Collections.Generic;
using Polly;
using PostMortem.Infrastructure.Queries;

namespace PostMortem.Domain.Projects
{
    public class GetAllProjectsQueryArgs : QueryArgsBase<IEnumerable<Project>>
    {

    }
}