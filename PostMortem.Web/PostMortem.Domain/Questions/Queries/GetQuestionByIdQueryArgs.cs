using System;
using PostMortem.Domain.EventSourcing.Queries;

namespace PostMortem.Domain.Questions.Queries
{
    public class GetQuestionByIdQueryArgs : QueryArgsBase<Question>
    {
        public Guid QuestionId { get; set; }
    }
}