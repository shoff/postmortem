namespace PostMortem.Domain.Comments.Commands
{
    using System;
    using Comments;
    using MediatR;
    using Polly;
    using Questions;
    using Zatoichi.EventSourcing;

    public class CommentAddedEvent : CommentCommandEvent, IRequest<PolicyResult>
    {
        public CommentAddedEvent(Comment comment)
        : base(comment)
        {
        }
        public override IEventEntity Apply(IEventEntity eventEntity)
        {
            throw new NotImplementedException();
            // ((Question)eventEntity).Comments.
        }

        public override void Apply()
        {
            throw new System.NotImplementedException();
        }
    }
}