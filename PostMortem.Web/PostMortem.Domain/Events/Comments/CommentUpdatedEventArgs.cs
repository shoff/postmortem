namespace PostMortem.Domain.Events.Comments
{
    using Domain.Comments;
    using Domain.Questions;
    using MediatR;
    using Polly;

    public class CommentUpdatedEventArgs : CommentEventArgs, IRequest<PolicyResult>
    {
        public CommentUpdatedEventArgs(Question question, Comment comment)
            : base(question, comment)
        {
        }
    }
}