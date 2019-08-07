namespace PostMortem.Infrastructure.Questions.Commands
{
    using System;
    using Domain.Questions;
    using Zatoichi.EventSourcing;

    public class AddQuestionCommand : QuestionCommand
    {
        public AddQuestionCommand(Question question)
            : this(question.QuestionId.Id, question.ProjectId, question.QuestionText)
        {
        }

        public AddQuestionCommand(Guid questionId, Guid projectId, string questionText)
            : base(questionId, projectId, questionText)
        {
            throw new NotImplementedException();
        }

        public void Apply(IEntity eventEntity)
        {
            throw new NotImplementedException();
        }
    }
}