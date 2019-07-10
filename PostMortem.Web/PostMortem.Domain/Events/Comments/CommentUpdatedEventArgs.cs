namespace PostMortem.Domain.Events.Comments
{
    using Domain.Comments;
    using Domain.Questions;
    using MediatR;

    public class CommentUpdatedEventArgs : CommentEventArgs, INotification
    {
        public CommentUpdatedEventArgs(Question question, Comment comment)
            : base(question, comment)
        {
        }
    }
}