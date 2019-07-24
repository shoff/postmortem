using System.Collections.Generic;
using PostMortem.Domain.EventSourcing.Queries;

namespace PostMortem.Domain.Questions.Queries
{
    public class GetAllQuestionsQueryArgs : QueryArgsBase<IEnumerable<Question>>
    {
    }
}