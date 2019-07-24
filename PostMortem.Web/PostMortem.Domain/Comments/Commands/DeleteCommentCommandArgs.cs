using System;
using PostMortem.Domain.EventSourcing.Commands;

namespace PostMortem.Domain.Comments.Commands
{
    public class DeleteCommentCommandArgs : CommandArgsBase
    {
        public Guid CommentId { get;  set; }
    }
}