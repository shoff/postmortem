namespace PostMortem.Domain.Comments.Commands
{
    using System;
    using Voters;
    using Zatoichi.EventSourcing;

    public class LikeCommentCommand : CommentCommand
    {
        public LikeCommentCommand(Comment comment, IVoterId voterId) 
            : base(comment)
        {
            this.VoterId = voterId;
            this.CommentId = comment.CommentId;
        }
        
        public Guid CommentId { get; }
        public IVoterId VoterId { get; }
        public override void Apply(IEventEntity eventEntity)
        {
            throw new NotImplementedException();
        }

    }
}