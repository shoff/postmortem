namespace PostMortem.Domain.Events.Comments
{
    using Domain.Comments;
    using MediatR;
    using Polly;

    public class CommentUpdatedEventArgs : CommentEventArgs, IRequest<PolicyResult>
    {
        public CommentUpdatedEventArgs(Comment comment)
            : base(comment)
        {
        }
    }
}