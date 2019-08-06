namespace PostMortem.Domain.Questions.Commands
{
    using System;
    using Questions;
    using Zatoichi.EventSourcing;

    public class UpdateQuestionCommand : QuestionCommand
    {
        public UpdateQuestionCommand(Question question)
            : base(question.QuestionId, question.ProjectId, question.QuestionText)
        {
        }

        public override void Apply(IEventEntity eventEntity)
        {
            throw new NotImplementedException();
        }
    }
}