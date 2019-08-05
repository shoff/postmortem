namespace PostMortem.Domain
{
    using System;
    using Comments;
    using Comments.Commands;
    using Comments.Queries;

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