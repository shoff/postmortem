namespace PostMortem.Domain.Comments.Events
{
    using Comments;

    public class CommentCommandAddedEvent : CommentCommandEvent
    {
        public CommentCommandAddedEvent(Comment comment)
        : base(comment)
        {
        }

    }
}