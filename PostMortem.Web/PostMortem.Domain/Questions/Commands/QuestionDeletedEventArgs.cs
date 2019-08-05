namespace PostMortem.Domain.Questions.Commands
{
    using System;
    using ChaosMonkey.Guards;
    using MediatR;
    using Polly;
    using Questions;

    public class QuestionDeletedEventArgs : EventArgs, IRequest<PolicyResult>
    {
        public QuestionDeletedEventArgs(Question question)
        {
            this.Question = Guard.IsNotNull(question, nameof(question));
        }

        public Question Question { get; set; }
    }
}