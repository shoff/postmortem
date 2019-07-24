using System;
using MediatR;
using Polly;
using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Domain.Comments
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