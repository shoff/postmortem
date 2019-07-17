namespace PostMortem.Domain.Events.Questions
{
    using System;
    using Domain.Questions;
    using MediatR;
    using Polly;

    public class QuestionDeletedEventArgs : Command<Question>, IRequest<PolicyResult>
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

        public override T Apply<T>()
        {
            throw new NotImplementedException();
        }
        public Guid QuestionId { get; private set; }

    }
}