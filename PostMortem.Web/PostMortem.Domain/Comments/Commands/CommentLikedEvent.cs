namespace PostMortem.Domain.Comments.Commands
{
    using System;

    public class CommentLikedEvent : CommentCommandEvent
    {
        public CommentLikedEvent(Comment comment)
            : base(comment)
        {
            this.CommentId = comment.CommentId;
        }

        public Guid CommentId { get; }
    }
}