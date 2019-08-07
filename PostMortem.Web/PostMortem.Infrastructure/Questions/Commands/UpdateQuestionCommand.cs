namespace PostMortem.Infrastructure.Questions.Commands
{
    using System;
    using Domain.Questions;
    using Zatoichi.EventSourcing;

    public class UpdateQuestionCommand : QuestionCommand
    {
        public UpdateQuestionCommand(Question question)
            : base(question.QuestionId.Id, question.ProjectId, question.QuestionText)
        {
        }

        public void Apply(IEntity eventEntity)
        {
            throw new NotImplementedException();
        }
    }
}