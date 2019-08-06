namespace PostMortem.Domain.Comments.Events
{
    using ChaosMonkey.Guards;
    using MediatR;

    public class CommentLiked : INotification
    {
        public Comment Comment { get; }

        public CommentLiked(Comment comment)
        {
            Comment = Guard.IsNotNull(comment, nameof(comment));
        }
    }
}