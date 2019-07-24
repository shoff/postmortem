using PostMortem.Domain.EventSourcing.Commands;

namespace PostMortem.Domain.Questions.Commands
{
    public class AddQuestionCommandArgs : CommandArgsBase
    {
        public AddQuestionCommandArgs(Question question) => Question = question;
        public Question Question { get; private set; }
    }
}