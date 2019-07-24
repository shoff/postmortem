using System;
using System.Collections.Generic;
using PostMortem.Domain.EventSourcing.Queries;

namespace PostMortem.Domain.Comments.Queries
{
    public class GetCommentsForQuestionQueryArgs : QueryArgsBase<IEnumerable<Comment>>
    {
        public Guid QuestionId { get; set; }
    }
}