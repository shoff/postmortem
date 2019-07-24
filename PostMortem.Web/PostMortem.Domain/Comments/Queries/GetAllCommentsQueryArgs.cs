using System.Collections.Generic;
using PostMortem.Domain.EventSourcing.Queries;

namespace PostMortem.Domain.Comments.Queries
{
    public class GetAllCommentsQueryArgs : QueryArgsBase<IEnumerable<Comment>>
    {
        
    }
}