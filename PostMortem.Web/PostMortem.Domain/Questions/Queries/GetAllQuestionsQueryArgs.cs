using System.Collections.Generic;
using PostMortem.Infrastructure.Queries;

namespace PostMortem.Domain.Questions.Queries
{
    public class GetAllQuestionsQueryArgs : QueryArgsBase<IEnumerable<Question>>
    {
    }
}