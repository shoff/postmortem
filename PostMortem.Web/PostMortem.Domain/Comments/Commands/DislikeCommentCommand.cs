﻿namespace PostMortem.Domain.Comments.Commands
{
    using System;
    using ChaosMonkey.Guards;
    using Voters;
    using Zatoichi.EventSourcing;

    public class DislikeCommentCommand : CommentCommand
    {

        public DislikeCommentCommand(Guid commentId, IVoterId voterId)
            : base(commentId)
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