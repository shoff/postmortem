namespace PostMortem.Domain.Comments.Commands
{
    using System;

    public class CommentDislikedEvent : CommentCommandEvent
    {
        public CommentDislikedEvent(Comment comment)
            : base(comment)
        {
            CommentId = comment.CommentId;
        }

        public Guid CommentId { get; set; }
    }
}