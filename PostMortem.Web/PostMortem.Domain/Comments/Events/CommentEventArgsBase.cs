using System;
using PostMortem.Infrastructure;
using PostMortem.Infrastructure.Events;

namespace PostMortem.Domain.Comments.Events
{
    public abstract class CommentEventArgsBase : EventArgsBase
    {
        protected CommentEventArgsBase(Guid commentId)
        {
            CommentId = commentId;
        }
        public Guid CommentId { get; private set; }
        public override IEntityId GetEntityId() => new CommentId(this.CommentId);
    }

    public abstract class CommentUpdateEventArgsBase<TProp> : CommentEventArgsBase, IUpdateEventArgs<TProp>
    {
        protected CommentUpdateEventArgsBase(Guid commentId, TProp oldValue, TProp newValue) :base(commentId)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
        public TProp NewValue { get; }
        public TProp OldValue { get; }
    }

}
