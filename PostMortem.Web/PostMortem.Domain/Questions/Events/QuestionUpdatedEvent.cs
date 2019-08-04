namespace PostMortem.Domain.Questions.Events
{
    using System;
    using ChaosMonkey.Guards;
    using MediatR;
    using Polly;
    using Questions;

    public class QuestionUpdatedEvent : EventArgs, IRequest<PolicyResult>
    {
        public QuestionUpdatedEvent(Question question)
        {
            this.Question = Guard.IsNotNull(question, nameof(question));
        }

        public Question Question { get; set; }
    }
}