namespace PostMortem.Domain.Events
{
    using System;
    using Comments;

    public interface IEventProducer
    {
        event EventHandler<CommentAddedEventArgs> CommentAddedEvent;
        event EventHandler<CommentLikedEventArgs> CommentLikedEvent;
        event EventHandler<CommentDislikedEventArgs> CommentDislikedEvent;
        //void SubscribeToProjectEvents();
        //void SubscribeToQuestionEvents();
        //void SubscribeToCommentEvents();
        //void UnsubscribeFromProjectEvents();
        //void UnsubscribeFromQuestionEvents();
        //void UnsubscribeFromCommentEvents();
    }
}