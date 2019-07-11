namespace PostMortem.Domain.Events
{
    using System;
    using Comments;
    using Projects;

    public interface IEventBroker
    {
        event EventHandler<ProjectCreatedEventArgs> ProjectCreated;
        event EventHandler<CommentDislikedEventArgs> CommentDisliked;
    }
}