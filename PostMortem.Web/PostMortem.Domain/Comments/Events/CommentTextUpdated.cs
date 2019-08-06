namespace PostMortem.Domain.Comments.Events
{
    using ChaosMonkey.Guards;

    public class CommentTextUpdated
    {
        public Comment Comment { get; }

        public CommentTextUpdated(Comment comment)
        {
            Comment = Guard.IsNotNull(comment, nameof(comment));
        }
    }
}