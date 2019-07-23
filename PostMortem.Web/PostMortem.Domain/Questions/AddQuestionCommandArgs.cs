﻿using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Domain.Events.Questions
{
    using System;
    using ChaosMonkey.Guards;
    using Domain.Questions;
    using MediatR;
    using Polly;

    public class AddQuestionCommandArgs : IRequest<PolicyResult>
    {
        public AddQuestionCommandArgs(Question question) => Question = question;
        public Question Question { get; private set; }
    }
}