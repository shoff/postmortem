namespace PostMortem.Domain.Events.Comments
{
    using System;
    using MediatR;
    using Polly;

    public class CommentDislikedEventArgs : EventArgs, IRequest<PolicyResult>
    {
        public CommentDislikedEventArgs(Guid commentId)
        {
            this.CommentId = commentId;
        }
        public Guid CommentId { get; set; }
    }
}