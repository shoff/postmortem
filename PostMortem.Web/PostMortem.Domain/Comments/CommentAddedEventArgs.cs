namespace PostMortem.Domain.Events.Comments
{
    using System;
    using Domain.Comments;
    using MediatR;
    using Polly;

    public class CommentAddedEventArgs : CommentEventArgsBase
    {
        public CommentAddedEventArgs(Guid commentId)
        : base(commentId)
        {
        }
        public Guid QuestionId { get; set; }
        public string CommentText { get; set; }
        public DateTime DateAdded { get; set; }
        public string Commenter { get; set; }

    }
}