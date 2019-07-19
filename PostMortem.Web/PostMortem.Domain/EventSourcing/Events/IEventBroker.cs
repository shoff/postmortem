using System;
using PostMortem.Domain.Events.Comments;
using PostMortem.Domain.Events.Projects;
using PostMortem.Domain.EventSourcing.Commands;
using PostMortem.Domain.EventSourcing.Queries;
using Remotion.Linq.Clauses;

namespace PostMortem.Domain.EventSourcing.Events
{
    public interface IEventBroker /* Should we rename this? It does more than event brokering */
    {
        void RaiseEvent<TArgs>(TArgs @event) where TArgs : IEventArgs;
        void DispatchCommand<TArgs>(TArgs command) where TArgs : ICommandArgs;
        //TOutput ExecuteQuery<TQueryArgs, TOutput>(TQueryArgs query) where TQueryArgs : IQueryArgs<TOutput>;
        //void RegisterEventHandler<TEventArgs,TEventHandler>(TEventHandler handler) : IEventHandler<TEventArgs>; /* do we need this? */
        //void RegisterCommandHandler<TCommandArgs,TCommandHandler>(TCommandHandler handler) : ICommandHandler<TCommandArgs>; /* do we need this? */
    }
}