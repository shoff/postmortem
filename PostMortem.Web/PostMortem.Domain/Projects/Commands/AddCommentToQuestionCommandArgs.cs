using System;
using System.Collections.Generic;
using System.Text;
using PostMortem.Infrastructure.EventSourcing.Commands;

namespace PostMortem.Infrastructure.Events.Projects
{
    public class AddCommentToQuestionCommandArgs : CommandArgsBase
    {
        public AddCommentToQuestionCommandArgs(Guid questionId, Guid commentId)
        {
            this.QuestionId = questionId;
            this.CommentId = commentId;
        }

        public Guid CommentId { get; private set; }

        public Guid QuestionId { get; private set; }
    }
    public class DeleteQuestionCommandArgs : CommandArgsBase
    {
        public Guid QuestionId { get; private set; }
    }
}
