namespace PostMortem.Domain.Events.Questions
{
    using System;
    using Domain.Questions;
    using MediatR;
    using Polly;

    public class QuestionDeletedEventArgs : Command<Question>, IRequest<PolicyResult>
    {
        public QuestionDeletedEventArgs() { }

        public QuestionDeletedEventArgs(QuestionId questionId)
        {
            this.QuestionId = questionId;
        }

        public override Question Apply(Question t)
        {
            t.Active = false;
            return t;
        }

        public QuestionId QuestionId { get; private set; }

    }
}