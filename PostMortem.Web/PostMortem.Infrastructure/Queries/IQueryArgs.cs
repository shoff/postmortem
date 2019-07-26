using MediatR;
using Polly;

namespace PostMortem.Infrastructure.Queries
{
    public interface IQueryArgs<TOut> : IRequest<PolicyResult<TOut>> {}
}