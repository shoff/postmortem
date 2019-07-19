using MediatR;

namespace PostMortem.Domain.EventSourcing.Commands
{
    public interface ICommandHandler<in TArgs> : IRequestHandler<TArgs> where TArgs : ICommandArgs{}
}