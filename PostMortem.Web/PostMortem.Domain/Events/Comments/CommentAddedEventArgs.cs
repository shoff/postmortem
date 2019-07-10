namespace PostMortem.Domain.Events.Comments
{
    using Domain.Comments;
    using MediatR;
    using Polly;

    public class CommentAddedEventArgs : CommentEventArgs, IRequest<PolicyResult>
    {
        public CommentAddedEventArgs(Comment comment)
        : base(comment)
        {
        }

    }
}