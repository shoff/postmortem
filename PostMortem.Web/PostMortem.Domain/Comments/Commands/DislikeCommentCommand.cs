namespace PostMortem.Domain.Comments.Commands
{
    using System;
    using Voters;
    using Zatoichi.EventSourcing;

    public class DislikeCommentCommand : CommentCommand
    {

        public DislikeCommentCommand(Comment comment, IVoterId voterId) 
            : base(comment)
        {
            this.CommentId = comment.CommentId;
            this.VoterId = voterId;
        }

        public IVoterId VoterId { get; }
        public Guid CommentId { get; }
        public override void Apply(IEventEntity eventEntity)
        {
            throw new NotImplementedException();
        }
    }
}