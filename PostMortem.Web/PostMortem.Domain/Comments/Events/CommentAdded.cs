namespace PostMortem.Domain.Comments.Events
{
    using ChaosMonkey.Guards;
    using MediatR;

    public class CommentAdded : INotification
    {
        public Comment Comment { get; }

        public CommentAdded(Comment comment)
        {
            Comment = Guard.IsNotNull(comment, nameof(comment));
        }
    }
}