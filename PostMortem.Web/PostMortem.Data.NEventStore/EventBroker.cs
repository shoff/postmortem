namespace PostMortem.Data.EventSourcing
{
    using System;
    using Domain.Events;
    using Domain.Events.Comments;
    using Domain.Events.Projects;

    public class EventBroker : IEventBroker
    {
        public event EventHandler<ProjectCreatedEventArgs> ProjectCreated;
        public event EventHandler<CommentDislikedEventArgs> CommentDisliked;
    }
}