using System;
using PostMortem.Domain.EventSourcing.Commands;

namespace PostMortem.Domain.Comments.Commands
{
    public class CreateCommentCommandArgs : CommandArgsBase
    {
        public CreateCommentCommandArgs(){}

        public CreateCommentCommandArgs(Comment comment)
        {
            CommentId = comment.CommentId;
            QuestionId = comment.QuestionId;
            CommentText = comment.CommentText;
            Commenter = comment.Commenter;
            Dislikes = comment.Dislikes;
            Likes = comment.Likes;
            GenerallyPositive = comment.GenerallyPositive;
            DateAdded = comment.DateAdded;
        }
        public Guid CommentId { get;  private set; }
        public Guid QuestionId { get;  private set; }
        public string CommentText { get;  private set; }
        public DateTime DateAdded { get;  private set; }
        public string Commenter { get;  private set; }
        public int Dislikes { get; private set; }
        public int Likes { get; private set; }
        public bool GenerallyPositive { get; private set; }    }
}