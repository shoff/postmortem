using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Polly;

namespace PostMortem.Domain.EventSourcing.Events
{
    public interface IEventHandler<in TEventArgs> : IRequestHandler<TEventArgs,PolicyResult>//: INotificationHandler<TArgs>
        where TEventArgs : IEventArgs
    {
        
    }
}