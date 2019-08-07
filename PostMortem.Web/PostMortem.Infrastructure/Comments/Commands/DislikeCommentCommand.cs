namespace PostMortem.Infrastructure.Comments.Commands
{
    using System;
    using ChaosMonkey.Guards;
    using Domain.Voters;
    using Zatoichi.EventSourcing;

    public class DislikeCommentCommand : CommentCommand
    {

        public DislikeCommentCommand(Guid commentId, IVoterId voterId)
            : base(commentId)
        {
            this.VoterId = Guard.IsNotNull(voterId, nameof(voterId));
        }

        public IVoterId VoterId { get; }

        public void Apply(IEntity eventEntity)
        {
            throw new NotImplementedException();
        }
    }
}