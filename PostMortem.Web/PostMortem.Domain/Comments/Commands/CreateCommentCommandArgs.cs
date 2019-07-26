using System;
using PostMortem.Infrastructure.EventSourcing.Commands;

namespace PostMortem.Domain.Comments.Commands
{
    public class CreateCommentCommandArgs : CommandArgsBase
    {
        public Guid CommentId { get;  set; } = Guid.NewGuid();
        public Guid QuestionId { get;  set; }
        public string CommentText { get;  set; }
        public DateTime DateAdded { get;  set; }
        public string Commenter { get;  set; }
        public int Dislikes { get; set; } = 0;
        public int Likes { get; set; } = 0;
        public bool GenerallyPositive { get; set; } = true;
    }
}