using System;
using System.Text;
using PostMortem.Infrastructure.Queries;

namespace PostMortem.Domain.Comments.Queries
{
    public class GetCommentByIdQueryArgs : QueryArgsBase<Comment>
    {
        public Guid CommentId { get; set; }
    }
}
