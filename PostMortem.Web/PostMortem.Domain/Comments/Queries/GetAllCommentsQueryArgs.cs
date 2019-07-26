using System.Collections.Generic;
using PostMortem.Infrastructure.Queries;

namespace PostMortem.Domain.Comments.Queries
{
    public class GetAllCommentsQueryArgs : QueryArgsBase<IEnumerable<Comment>>
    {
        
    }
}