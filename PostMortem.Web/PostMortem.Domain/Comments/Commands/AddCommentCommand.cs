namespace PostMortem.Domain.Comments.Commands
{
    using System;
    using ChaosMonkey.Guards;
    using Voters;
    using Zatoichi.EventSourcing;

    public class AddCommentCommand : CommentCommand
    {
        public AddCommentCommand(
            Comment comment,
            IVoterId voterId)
            : base(comment.CommentId.Id)
        {
            this.VoterId = Guard.IsNotNull(voterId, nameof(voterId));
        }

        public IVoterId VoterId { get; }

        public override void Apply(IEventEntity eventEntity)
        {
            throw new NotImplementedException();
        }
    }
}