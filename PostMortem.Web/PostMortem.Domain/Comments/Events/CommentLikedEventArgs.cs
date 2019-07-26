using MediatR;
using Polly;
using PostMortem.Domain.Comments;
using PostMortem.Domain.Comments.Events;

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