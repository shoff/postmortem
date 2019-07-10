namespace PostMortem.Domain.Events.Comments
{
    using Domain.Comments;
    using Domain.Questions;
    using MediatR;
    using Polly;

    public class CommentAddedEventArgs : CommentEventArgs, IRequest<PolicyResult>
    {
        public CommentAddedEventArgs(Question question, Comment comment)
        : base(question, comment)
        {
        }

    }
}