namespace PostMortem.Domain.Events.Comments
{
    using Domain.Comments;
    using Domain.Questions;
    using MediatR;

    public class CommentAddedEventArgs : CommentEventArgs, INotification
    {
        public CommentAddedEventArgs(Question question, Comment comment)
        : base(question, comment)
        {
        }

    }
}