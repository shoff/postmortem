﻿using System.Collections.Generic;
using Polly;
using PostMortem.Domain.EventSourcing.Queries;

namespace PostMortem.Domain.Projects
{
    public class GetAllProjectsQueryArgs : IQueryArgs<PolicyResult<IEnumerable<Project>>>
    {

    }
}