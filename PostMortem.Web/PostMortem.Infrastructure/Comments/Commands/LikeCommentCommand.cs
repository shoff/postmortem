namespace PostMortem.Infrastructure.Comments.Commands
{
    using System;
    using ChaosMonkey.Guards;
    using Domain.Voters;
    using Zatoichi.EventSourcing;

    public class LikeCommentCommand : CommentCommand
    {
        public LikeCommentCommand(Guid commentId, IVoterId voterId) 
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