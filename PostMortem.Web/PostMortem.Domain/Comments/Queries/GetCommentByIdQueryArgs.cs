using System;
using System.Text;
using PostMortem.Domain.EventSourcing.Commands;
using PostMortem.Domain.EventSourcing.Queries;

namespace PostMortem.Domain.Comments.Queries
{
    public class GetCommentByIdQueryArgs : QueryArgsBase<Comment>
    {
        public Guid CommentId { get; set; }
    }
}
