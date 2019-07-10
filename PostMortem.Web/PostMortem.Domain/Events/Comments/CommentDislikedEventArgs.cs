namespace PostMortem.Domain.Events.Comments
{
    using System;
    using MediatR;
    using Polly;

    public class CommentDislikedEventArgs : EventArgs, IRequest<PolicyResult>
    {
        public Guid CommentId { get; set; }
    }
}