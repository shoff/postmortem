namespace PostMortem.Domain.Comments.Commands
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