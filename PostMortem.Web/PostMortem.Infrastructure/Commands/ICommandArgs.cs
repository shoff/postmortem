using MediatR;
using Polly;

namespace PostMortem.Infrastructure.EventSourcing.Commands
{
    public interface ICommandArgs : IRequest<PolicyResult>
    {
        
    }
}