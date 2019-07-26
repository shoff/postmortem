using System;
using System.Collections.Generic;
using PostMortem.Infrastructure.Queries;

namespace PostMortem.Domain.Comments.Queries
{
    public class GetCommentsForQuestionQueryArgs : QueryArgsBase<IEnumerable<Comment>>
    {
        public Guid QuestionId { get; set; }
    }
}