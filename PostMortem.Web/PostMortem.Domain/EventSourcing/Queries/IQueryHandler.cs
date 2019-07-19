using MediatR;

namespace PostMortem.Domain.EventSourcing.Queries
{
    public interface IQueryHandler<in TIn, TOut> : IRequestHandler<TIn, TOut>
        where TIn : IQueryArgs<TOut>
    {

    }
}