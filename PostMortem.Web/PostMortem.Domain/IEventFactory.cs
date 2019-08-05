namespace PostMortem.Domain
{
    using System;
    using Comments.Queries;

    public interface IEventFactory
    {
        CommentGetByIdEvent CreateEvent(Guid commentId);
    }
}