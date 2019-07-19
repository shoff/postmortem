using PostMortem.Domain.Comments;
using PostMortem.Domain.EventSourcing.Commands;
using PostMortem.Domain.EventSourcing.Events;
using PostMortem.Domain.Projects;

namespace PostMortem.Infrastructure.Events
{
    using System;
    using Domain.Events;
    using Domain.Events.Comments;
    using Domain.Events.Projects;
    using Domain.Events.Questions;
    using Zatoichi.Common.Infrastructure.Extensions;

    public class EventBroker : IEventBroker
    {
        //TODO: Patch in MediatR
        public void RaiseEvent<TArgs>(TArgs @event) where TArgs : IEventArgs
        {
            throw new NotImplementedException();
        }

        public void DispatchCommand<TArgs>(TArgs command) where TArgs : ICommandArgs
        {
            throw new NotImplementedException();
        }
    }
}