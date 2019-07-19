using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Domain.Events.Questions
{
    using System;
    using System.Linq.Expressions;
    using ChaosMonkey.Guards;
    using Domain.Questions;
    using MediatR;
    using Newtonsoft.Json;
    using Polly;

    //public class QuestionImportanceSetEventArgs : UpdateEventArgsBase<int>
    //{
    //    public Guid QuestionId { get; set; }

    //}
}