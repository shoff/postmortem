namespace PostMortem.Infrastructure.Events
{
    using System;
    using Domain.Events;
    using Domain.Events.Comments;
    using Domain.Events.Projects;
    using Zatoichi.Common.Infrastructure.Extensions;

    public class EventBroker : IEventBroker
    {
        public event EventHandler<ProjectCreatedEventArgs> ProjectCreated;
        public event EventHandler<CommentDislikedEventArgs> CommentDisliked;


        public void DislikeComment(object sender, CommentDislikedEventArgs eventArgs)
        {
            this.CommentDisliked.Raise(sender, eventArgs);
        }
    }
}