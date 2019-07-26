using PostMortem.Infrastructure.EventSourcing.Commands;

namespace PostMortem.Domain.Questions.Commands
{
    public class AddQuestionToProjectCommandArgs : CommandArgsBase
    {
        public AddQuestionToProjectCommandArgs(Question question) => Question = question;
        public Question Question { get; private set; }
    }
}