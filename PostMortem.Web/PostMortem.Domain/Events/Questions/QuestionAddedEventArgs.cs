namespace PostMortem.Domain.Events.Questions
{
    using System;
    using ChaosMonkey.Guards;
    using Domain.Questions;
    using MediatR;
    using Polly;

    public class QuestionAddedEventArgs : Command<Question>, IRequest<PolicyResult<Guid>>
    {
        public QuestionAddedEventArgs(Question question)
        {
            this.Question = Guard.IsNotNull(question, nameof(question));
        }

        public Question Question { get; set; }

        public override Question Apply(Question t)
        {
            throw new NotImplementedException();
        }

    }
}