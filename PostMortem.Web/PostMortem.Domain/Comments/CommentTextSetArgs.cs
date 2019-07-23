using System;
using MediatR;
using Polly;
using PostMortem.Domain.Comments;
using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Domain.Events.Comments
{
    public class CommentTextSetArgs : CommentEventArgsBase,IUpdateEventArgs<string>
    {
        public CommentTextSetArgs(Guid commentId, string oldValue,string newValue) :base(commentId)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
        public string NewValue { get; }
        public string OldValue { get; }
    }
}