namespace PostMortem.Domain.Comments.Events
{
    public class CommentCommandReplacedEvent : CommentCommandEvent
    {
        public CommentCommandReplacedEvent(Comment comment)
            : base(comment)
        {
        }

    }
}