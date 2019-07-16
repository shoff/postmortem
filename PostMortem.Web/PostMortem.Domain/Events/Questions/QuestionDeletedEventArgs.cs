namespace PostMortem.Domain.Events.Questions
{
    using System;
    using Domain.Questions;
    using MediatR;
    using Polly;

    public class QuestionDeletedEventArgs : EventBase<Question>, IRequest<PolicyResult>
    {
        public QuestionDeletedEventArgs() { }

        public QuestionDeletedEventArgs(Guid questionId)
        {
            this.QuestionId = questionId;
        }

        public override Question Apply(Question t)
        {
            t.Active = false;
            return t;
        }

        public override Question Undo(Question t)
        {
            t.Active = true;
            return t;
        }

        public Guid QuestionId { get; private set; }

    }
}