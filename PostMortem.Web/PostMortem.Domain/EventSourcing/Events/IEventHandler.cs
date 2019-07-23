using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace PostMortem.Domain.EventSourcing.Events
{
    public interface IEventHandler<in TEventArgs> //: INotificationHandler<TArgs>
        where TEventArgs : IEventArgs
    {
        Task Handle(TEventArgs args, CancellationToken cancellationToken);
    }
}