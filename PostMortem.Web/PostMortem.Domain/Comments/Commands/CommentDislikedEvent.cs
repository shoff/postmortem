namespace PostMortem.Domain.Comments.Commands
{
    using System;
    using MediatR;
    using Polly;
    using Voters;
    using Zatoichi.EventSourcing;

    public class CommentDislikedEvent : CommentCommandEvent, IRequest<PolicyResult>
    {

        public CommentDislikedEvent(Comment comment, IVoterId voterId)
            : base(comment)
        {
            this.VoterId = voterId;
        }

        public IVoterId VoterId { get; }
        public Guid CommentId { get; set; }

        public override void Apply()
        {

        }

        public override IEventEntity Apply(IEventEntity eventEntity)
        {
            throw new NotImplementedException();
        }
    }
}