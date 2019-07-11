namespace PostMortem.Domain.Events
{
    using System;
    using Comments;
    using Projects;

    public interface IEventBroker
    {
        event EventHandler<ProjectCreatedEventArgs> ProjectCreated;
        event EventHandler<CommentDislikedEventArgs> CommentDisliked;


        void CreateProject(object sender, ProjectCreatedEventArgs eventArgs);
        void DislikeComment(object sender, CommentDislikedEventArgs eventArgs);
        void LikeComment(object sender, CommentLikedEventArgs eventArgs);
    }
}