namespace PostMortem.Domain.Questions.Events
{
    using System;
    using ChaosMonkey.Guards;
    using MediatR;
    using Polly;
    using Questions;

    public class QuestionUpdatedEventArgs : EventArgs, IRequest<PolicyResult>
    {
        public QuestionUpdatedEventArgs(Question question)
        {
            this.Question = Guard.IsNotNull(question, nameof(question));
        }

        public Question Question { get; set; }
    }
}