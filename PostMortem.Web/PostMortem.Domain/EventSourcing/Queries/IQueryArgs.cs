using MediatR;

namespace PostMortem.Domain.EventSourcing.Queries
{
    public interface IQueryArgs<out TOut> : IRequest<TOut> {}
}