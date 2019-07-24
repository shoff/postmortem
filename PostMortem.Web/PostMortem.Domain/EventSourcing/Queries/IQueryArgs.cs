using MediatR;
using Polly;

namespace PostMortem.Domain.EventSourcing.Queries
{
    public interface IQueryArgs<TOut> : IRequest<PolicyResult<TOut>> {}
}