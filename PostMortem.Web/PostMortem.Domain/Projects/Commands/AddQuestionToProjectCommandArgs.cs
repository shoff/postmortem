using System;
using System.Collections.Generic;
using System.Text;
using PostMortem.Domain.EventSourcing.Commands;

namespace PostMortem.Infrastructure.Events.Projects
{
    public class AddQuestionToProjectCommandArgs : CommandArgsBase
    {
        public AddQuestionToProjectCommandArgs(Guid questionId, Guid projectId)
        {
            this.QuestionId = questionId;
            this.ProjectId = projectId;
        }

        public Guid ProjectId { get; private set; }

        public Guid QuestionId { get; private set; }
    }
}
