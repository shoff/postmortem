using System;
using PostMortem.Infrastructure.Queries;

namespace PostMortem.Domain.Questions.Queries
{
    public class GetQuestionByIdQueryArgs : QueryArgsBase<Question>
    {
        public Guid QuestionId { get; set; }
    }
}