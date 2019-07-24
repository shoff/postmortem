using MediatR;
using Polly;
using PostMortem.Domain.Comments;

namespace PostMortem.Domain.Events.Comments
{
    using System;

    public class CommentLikedEventArgs : CommentEventArgsBase
    {
        public CommentLikedEventArgs(Guid commentId) :base(commentId)
        {
            
        }
    }
}