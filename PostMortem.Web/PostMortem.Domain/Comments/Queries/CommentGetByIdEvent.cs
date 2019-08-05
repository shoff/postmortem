namespace PostMortem.Domain.Comments.Queries
{
    using System;

    public class CommentGetByIdEvent : CommentQueryEvent
    {
        public Guid CommentId { get; }

        public CommentGetByIdEvent(Guid commentId)
        {
            CommentId = commentId;
            this.Description = "Gets a comment by id";
        }
    }
}