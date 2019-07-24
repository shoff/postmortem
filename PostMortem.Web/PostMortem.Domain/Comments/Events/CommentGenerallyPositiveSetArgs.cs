using System;
using PostMortem.Domain.Comments;
using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Domain.Events.Comments
{
    public class CommentGenerallyPositiveSetArgs : CommentEventArgsBase, IUpdateEventArgs<bool>
    {
        public CommentGenerallyPositiveSetArgs(Guid commentId, bool oldValue,bool newValue) :base(commentId)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
        public bool NewValue { get; }
        public bool OldValue { get; }
    }
}