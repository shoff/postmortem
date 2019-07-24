namespace PostMortem.Domain.Events.Comments
{
    using System;
    using Domain.Comments;
    using MediatR;
    using Polly;

    public class CommentGetByIdEventArgs : EventArgs, IRequest<PolicyResult<Comment>>
    {
        public Guid CommentId { get; }

        public CommentGetByIdEventArgs(Guid commentId)
        {
            CommentId = commentId;
        }
    }
}