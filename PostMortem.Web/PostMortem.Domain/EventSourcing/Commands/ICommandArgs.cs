using MediatR;
using Polly;

namespace PostMortem.Domain.EventSourcing.Commands
{
    public interface ICommandArgs : IRequest<PolicyResult>
    {
        
    }
}