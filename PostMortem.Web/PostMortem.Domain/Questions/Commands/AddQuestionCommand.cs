namespace PostMortem.Domain.Questions.Commands
{
    using System;
    using Questions;
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

        public override void Apply(IEventEntity eventEntity)
        {
            throw new NotImplementedException();
        }
    }
}