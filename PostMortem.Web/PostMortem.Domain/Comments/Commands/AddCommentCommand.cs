namespace PostMortem.Domain.Comments.Commands
{
    using Questions;
    using Zatoichi.EventSourcing;

    public class AddCommentCommand : CommentCommand
    {
        public AddCommentCommand(Comment comment)
            : base(comment)
        {
        }

        public override void Apply(IEventEntity eventEntity)
        {
            ((Question) eventEntity).AddComment(Comment);
        }
    }
}