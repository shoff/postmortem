namespace PostMortem.Domain.Events.Comments
{
    using System;
    using MediatR;
    using Polly;

    public class CommentLikedEventArgs : EventArgs, IRequest<PolicyResult>
    {
        public CommentLikedEventArgs(Guid commentId)
        {
            this.CommentId = commentId;
        }

        public Guid CommentId { get; private set; } 
    }
}