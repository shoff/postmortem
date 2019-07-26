using PostMortem.Domain.Comments.Events;

namespace PostMortem.Domain.Events.Comments
{
    using System;
    using Domain.Comments;
    using MediatR;
    using Polly;

    public class CommentCreatedEventArgs : CommentEventArgsBase
    {
        public CommentCreatedEventArgs(CommentId commentId, Guid questionId, string commenter, string commentText, DateTime dateAdded, int likes, int dislikes, bool generallyPositive)
        : base(commentId)
        {
            QuestionId = questionId;
            CommentText = commentText;
            Commenter = commenter;
            DateAdded = dateAdded;
            Dislikes = dislikes;
            Likes = likes;
            GenerallyPositive = generallyPositive;
        }
        public Guid QuestionId { get;  private set; }
        public string CommentText { get;  private set; }
        public DateTime DateAdded { get;  private set; }
        public string Commenter { get;  private set; }
        public int Dislikes { get; private set; }
        public int Likes { get; private set; }
        public bool GenerallyPositive { get; private set; }

    }
}