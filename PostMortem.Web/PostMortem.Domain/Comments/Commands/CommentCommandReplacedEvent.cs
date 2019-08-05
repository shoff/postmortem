namespace PostMortem.Domain.Comments.Commands
{
    public class CommentCommandReplacedEvent : CommentCommandEvent
    {
        public CommentCommandReplacedEvent(Comment comment)
            : base(comment)
        {
        }

        public override void Apply()
        {
            throw new System.NotImplementedException();
        }
    }
}