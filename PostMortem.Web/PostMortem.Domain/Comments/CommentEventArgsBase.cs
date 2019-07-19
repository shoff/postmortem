using System;
using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Domain.Comments
{
    public abstract class CommentEventArgsBase : EventArgsBase
    {
        protected CommentEventArgsBase(Guid commentId)
        {
            CommentId = commentId;
        }
        public Guid CommentId { get; set; }
        public override IEntityId GetEntityId() => new CommentId(CommentId);
    }
}