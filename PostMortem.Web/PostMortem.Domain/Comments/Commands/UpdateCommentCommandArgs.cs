using System;
using PostMortem.Domain.EventSourcing.Commands;

namespace PostMortem.Domain.Comments.Commands
{
    public class UpdateCommentCommandArgs : CommandArgsBase
    {
        public UpdateCommentCommandArgs()
        {
        }

        public UpdateCommentCommandArgs(Comment comment)
        {
            CommentId = comment.CommentId;
            CommentText = comment.CommentText;
        }

        public Guid CommentId { get; private set; }
        public string CommentText { get; private set; }
    }
}