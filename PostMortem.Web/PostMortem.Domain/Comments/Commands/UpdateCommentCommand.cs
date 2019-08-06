namespace PostMortem.Domain.Comments.Commands
{
    using Comments;
    using Zatoichi.EventSourcing;

    public class UpdateCommentCommand : CommentCommand
    {

        public UpdateCommentCommand(Comment comment) 
            : base(comment)
        {
        }

        public override void Apply(IEventEntity eventEntity)
        {
            throw new System.NotImplementedException();
        }

    }
}