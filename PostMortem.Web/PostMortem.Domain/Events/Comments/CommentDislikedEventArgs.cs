namespace PostMortem.Domain.Events.Comments
{
    using System;
    using MediatR;
    using Polly;

    public class CommentDislikedEventArgs : EventBase<Guid>, IRequest<PolicyResult>
    {
        public CommentDislikedEventArgs(Guid commentId)
        {
            this.CommentId = commentId;
        }
        public Guid CommentId { get; set; }
        public override Guid Apply(Guid t)
        {
            throw new NotImplementedException();
        }

        public override Guid Undo(Guid t)
        {
            throw new NotImplementedException();
        }
    }
}