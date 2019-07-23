namespace PostMortem.Domain.Events.Comments
{
    using System;
    using Domain.Comments;
    using MediatR;
    using Polly;

    public class CommentCreatedEventArgs : CommentEventArgsBase
    {
        public CommentCreatedEventArgs(CommentId commentId, Guid questionId, string commenter, string commentText, DateTime dateAdded)
        : base(commentId)
        {
            QuestionId = questionId;
            CommentText = commentText;
            Commenter = commenter;
            DateAdded = dateAdded;
        }
        public Guid QuestionId { get;  private set; }
        public string CommentText { get;  private set; }
        public DateTime DateAdded { get;  private set; }
        public string Commenter { get;  private set; }

    }
}