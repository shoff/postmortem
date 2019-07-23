using MediatR;

namespace PostMortem.Domain.EventSourcing.Queries
{
    public interface IQueryHandler<in TQueryArgs, TResult> //: IRequestHandler<TIn, TOut>
        where TQueryArgs : IQueryArgs<TResult>
    {
        TResult Handle(TQueryArgs query);
    }
}