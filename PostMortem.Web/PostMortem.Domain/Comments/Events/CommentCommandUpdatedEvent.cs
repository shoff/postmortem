namespace PostMortem.Domain.Comments.Events
{
    using Comments;

    public class CommentCommandUpdatedEvent : CommentCommandEvent
    {
        public CommentCommandUpdatedEvent(Comment comment)
            : base(comment)
        {
        }
    }
}