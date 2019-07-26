using System;
using System.Collections.Generic;
using PostMortem.Infrastructure.Queries;

namespace PostMortem.Domain.Questions.Queries
{
    public class GetQuestionsForProjectIdQueryArgs : QueryArgsBase<IEnumerable<Question>>
    {
        public Guid ProjectId { get; set; }
    }
}