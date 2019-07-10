namespace PostMortem.Infrastructure.Events
{
    using System;
    using Domain.Events;
    using Domain.Events.Comments;

    public class EventProducer : IEventProducer
    {
        public event EventHandler<CommentAddedEventArgs> CommentAddedEvent;
        public event EventHandler<CommentLikedEventArgs> CommentLikedEvent;
        public event EventHandler<CommentDislikedEventArgs> CommentDislikedEvent;

    }
}