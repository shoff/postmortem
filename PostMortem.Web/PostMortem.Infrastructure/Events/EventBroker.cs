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
        public event EventHandler<CommentLikedEventArgs> CommentLiked;


        public void CreateProject(object sender, ProjectCreatedEventArgs eventArgs)
        {
            this.ProjectCreated.Raise(sender, eventArgs);
        }

        public void DislikeComment(object sender, CommentDislikedEventArgs eventArgs)
        {
            this.CommentDisliked.Raise(sender, eventArgs);
        }

        public void LikeComment(object sender, CommentLikedEventArgs eventArgs)
        {
            this.CommentLiked.Raise(sender, eventArgs);
        }
    }
}