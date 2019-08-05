namespace PostMortem.Domain.Comments.Commands
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