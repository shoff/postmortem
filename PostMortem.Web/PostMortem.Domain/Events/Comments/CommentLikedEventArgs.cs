namespace PostMortem.Domain.Events.Comments
{
    using System;
    using Domain.Comments;
    using MediatR;
    using Polly;

    public class CommentLikedEventArgs : EventBase<Guid>, IRequest<PolicyResult>
    {
        public CommentLikedEventArgs(Guid commentId)
        {
            this.CommentId = commentId;
        }

        public Guid CommentId { get; private set; }


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