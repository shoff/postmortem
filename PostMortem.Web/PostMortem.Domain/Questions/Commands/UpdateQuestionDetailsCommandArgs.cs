using System;
using System.Collections.Generic;
using System.Text;
using PostMortem.Domain.EventSourcing.Commands;

namespace PostMortem.Domain.Questions.Commands
{
    public class UpdateQuestionDetailsCommandArgs: CommandArgsBase
    {
        public QuestionId QuestionId { get; set; }
        public Guid ProjectId { get; set; }
        public string QuestionText { get; set; }
        public int Importance { get; set; }
        public bool Active { get; set; } = true;
        public DateTime? LastUpdated { get; set; }
    }
}
