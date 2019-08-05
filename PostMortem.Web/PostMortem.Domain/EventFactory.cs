namespace PostMortem.Domain
{
    using System;
    using Comments;
    using Comments.Events;

    public class EventFactory : IEventFactory
    {
        public CommentGetByIdEvent CreateEvent(Guid commentId)
        {
            return new CommentGetByIdEvent(commentId);
        }

        public CommentCommandAddedEvent CreateEvent(Comment comment)
        {
            return new CommentCommandAddedEvent(comment);
        }
    }
}