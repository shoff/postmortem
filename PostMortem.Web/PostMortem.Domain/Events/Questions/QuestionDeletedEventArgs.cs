namespace PostMortem.Domain.Events.Questions
{
    using System;
    using ChaosMonkey.Guards;
    using Domain.Questions;
    using MediatR;
    using Polly;

    public class QuestionDeletedEventArgs : EventArgs, IRequest<PolicyResult>
    {
        public QuestionDeletedEventArgs(Question question)
        {
            this.Question = Guard.IsNotNull(question, nameof(question));
        }

        public Question Question { get; set; }
    }
}