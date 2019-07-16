﻿namespace PostMortem.Domain.Events.Questions
{
    using System;
    using Domain.Questions;
    using MediatR;
    using Polly;

    public class QuestionTextChangedEventArgs : EventBase<Question>, IRequest<PolicyResult>
    {
        public QuestionTextChangedEventArgs() { }

        public QuestionTextChangedEventArgs(Guid questionId, string text)
        {
            this.QuestionId = questionId;
            Text = text;
        }

        public Guid QuestionId { get; set; }
        public string Text { get; set; }


        public override Question Apply(Question t)
        {
            t.QuestionText = this.Text;
            t.LastUpdated = DateTime.UtcNow;
            return t;
        }

        public override Question Undo(Question t)
        {
            throw new System.NotImplementedException();
        }
    }
}