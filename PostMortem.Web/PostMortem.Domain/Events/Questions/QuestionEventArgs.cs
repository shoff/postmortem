namespace PostMortem.Domain.Events.Questions
{
    using System;
    using ChaosMonkey.Guards;
    using Domain.Questions;
    using MediatR;
    using Polly;

    public abstract class QuestionEventArgs : EventArgs, IRequest<PolicyResult>
    {
        protected QuestionEventArgs(Question question)
        {
            this.Question = Guard.IsNotNull(question, nameof(question));
        }

        public virtual DateTime CreatedDate => DateTime.UtcNow;
        public virtual Question Question { get; private set; }
    }
}