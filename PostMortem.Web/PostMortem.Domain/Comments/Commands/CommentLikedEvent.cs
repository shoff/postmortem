namespace PostMortem.Domain.Comments.Commands
{
    using System;
    using Voters;

    public class CommentLikedEvent : CommentCommandEvent
    {
        public CommentLikedEvent(Comment comment, IVoterId voterId)
            : base(comment)
        {
            this.VoterId = voterId;
            this.CommentId = comment.CommentId;
        }

        public Guid CommentId { get; }
        public IVoterId VoterId { get; }

        public override void Apply()
        {
            throw new NotImplementedException();
        }
    }
}