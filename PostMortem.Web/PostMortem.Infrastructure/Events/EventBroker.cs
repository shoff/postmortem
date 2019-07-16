namespace PostMortem.Infrastructure.Events
{
    using System;
    using Domain.Events;
    using Domain.Events.Comments;
    using Domain.Events.Projects;
    using Domain.Events.Questions;
    using Zatoichi.Common.Infrastructure.Extensions;

    public class EventBroker : IEventBroker
    {
        public event EventHandler<ProjectCreatedEventArgs> ProjectCreated;
        public event EventHandler<CommentDislikedEventArgs> CommentDisliked;
        public event EventHandler<CommentLikedEventArgs> CommentLiked;
        public event EventHandler<QuestionResponseCountChangedEventArgs> QuestionResponseCountChanged;
        public event EventHandler<QuestionImportanceEventArgs> QuestionImportanceChanged;

        public void ChangeResponseCount(object sender, QuestionImportanceEventArgs eventArgs)
        {
            this.QuestionImportanceChanged.Raise(sender, eventArgs);
        }

        public void ChangeResponseCount(object sender, QuestionResponseCountChangedEventArgs eventArgs)
        {
            this.QuestionResponseCountChanged.Raise(sender, eventArgs);
        }

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