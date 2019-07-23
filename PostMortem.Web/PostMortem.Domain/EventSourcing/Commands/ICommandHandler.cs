using MediatR;

namespace PostMortem.Domain.EventSourcing.Commands
{
    public interface ICommandHandler<in TCommandArgs> //: IRequestHandler<TArgs>
        where TCommandArgs : ICommandArgs
    {
        void Handle(TCommandArgs command);
    }
}