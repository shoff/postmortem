namespace PostMortem.Domain.Events.Comments
{
    using System;
    using Domain.Comments;
    using MediatR;
    using Polly;

    public class CommentAddedEventArgs : CommentEventArgs, IRequest<PolicyResult<Guid>>
    {
        public CommentAddedEventArgs(Comment comment)
        : base(comment)
        {
        }

    }
}