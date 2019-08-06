namespace PostMortem.Domain.Comments.Commands
{
    using System;
    using Zatoichi.EventSourcing;

    public class ReplaceCommentCommand : CommentCommand
    {
        public ReplaceCommentCommand(Comment comment) : base(comment)
        {
        }

        public override void Apply(IEventEntity eventEntity)
        {
            throw new NotImplementedException();
        }
    }
}