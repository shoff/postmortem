namespace PostMortem.Domain.Comments.Events
{
    using ChaosMonkey.Guards;
    using MediatR;

    public class CommentDisliked : INotification
    {
        public Comment Comment { get; }

        public CommentDisliked(Comment comment)
        {
            Comment = Guard.IsNotNull(comment, nameof(comment));
        }

    }
}