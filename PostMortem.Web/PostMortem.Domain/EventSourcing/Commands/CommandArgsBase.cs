using MediatR;
using Polly;

namespace PostMortem.Domain.EventSourcing.Commands
{
    public abstract class CommandArgsBase : ICommandArgs
    {
    }
}