using MediatR;
using Polly;

namespace PostMortem.Infrastructure.Queries
{
    public interface IQueryHandler<in TQueryArgs, TResult> : IRequestHandler<TQueryArgs, PolicyResult<TResult>>
        where TQueryArgs : IQueryArgs<TResult>
    {
        
    }
}