using System;
using PostMortem.Infrastructure.EventSourcing.Commands;

namespace PostMortem.Domain.Comments.Commands
{
    public class AddCommentToQuestionCommandArgs : CommandArgsBase
    {
        public Guid CommentId { get; set; }
        public Guid QuestionId { get; set; }

    }
}
