using MediatR;
using Polly;

namespace PostMortem.Domain.EventSourcing.Queries
{
    public interface IQueryHandler<in TQueryArgs, TResult> : IRequestHandler<TQueryArgs, PolicyResult<TResult>>
        where TQueryArgs : IQueryArgs<TResult>
    {
        
    }
}