namespace PostMortem.Domain
{
    using System;
    using Comments;
    using Comments.Events;

    public interface IEventFactory
    {
        CommentGetByIdEvent CreateEvent(Guid commentId);
    }
}