using System;
using MediatR;
using Polly;

namespace PostMortem.Domain.Comments
{
    public class CommentDislikedEventArgs : CommentEventArgsBase
    {
        public CommentDislikedEventArgs(Guid commentId) : base(commentId) {}
    }
}