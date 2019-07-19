using PostMortem.Domain.Comments;
using PostMortem.Domain.EventSourcing.Events;

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