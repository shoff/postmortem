namespace PostMortem.Domain.Questions.Commands
{
    using System;
    using Zatoichi.EventSourcing;

    public class DeleteQuestionCommand : QuestionCommand
    {
        public DeleteQuestionCommand(Question question)
            : base(question.QuestionId, question.ProjectId, question.QuestionText)
        {
        }

        public override void Apply(IEventEntity eventEntity)
        {
            throw new NotImplementedException();
        }
    }
}