using System;
using MediatR;
using Polly;
using PostMortem.Domain.Comments.Events;

namespace PostMortem.Domain.Comments
{
    public class CommentDislikedEventArgs : CommentEventArgsBase
    {
        public CommentDislikedEventArgs(Guid commentId) : base(commentId) {}
    }
}