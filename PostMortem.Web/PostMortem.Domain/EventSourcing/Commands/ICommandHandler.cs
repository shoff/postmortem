using MediatR;
using Polly;

namespace PostMortem.Domain.EventSourcing.Commands
{
    public interface ICommandHandler<in TCommandArgs> : IRequestHandler<TCommandArgs,PolicyResult>
        where TCommandArgs : ICommandArgs
    {
        
    }
}