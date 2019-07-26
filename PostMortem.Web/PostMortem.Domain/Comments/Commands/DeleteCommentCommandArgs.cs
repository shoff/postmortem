using System;
using PostMortem.Infrastructure.EventSourcing.Commands;

namespace PostMortem.Domain.Comments.Commands
{
    public class DeleteCommentCommandArgs : CommandArgsBase
    {
        public Guid CommentId { get;  set; }
    }
}