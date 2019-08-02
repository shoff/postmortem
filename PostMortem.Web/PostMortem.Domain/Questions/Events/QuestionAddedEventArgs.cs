namespace PostMortem.Domain.Questions.Events
{
    using System;
    using ChaosMonkey.Guards;
    using MediatR;
    using Polly;
    using Questions;

    public class QuestionAddedEventArgs : EventArgs, IRequest<PolicyResult<Guid>>
    {
        public QuestionAddedEventArgs(Question question)
        {
            this.Question = Guard.IsNotNull(question, nameof(question));
        }

        public Question Question { get; set; }
    }
}