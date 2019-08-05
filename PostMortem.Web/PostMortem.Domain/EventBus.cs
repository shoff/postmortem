namespace PostMortem.Domain
{
    using System.Threading.Tasks;
    using Zatoichi.Common.Infrastructure.Services;
    using Zatoichi.EventSourcing.Commands;
    using Zatoichi.EventSourcing.Queries;

    public class EventBus
    {
        private readonly IQueryBus queryBus;
        private readonly ICommandBus commandBus;

        public EventBus(
            IQueryBus queryBus,
            ICommandBus commandBus)
        {
            this.queryBus = queryBus;
            this.commandBus = commandBus;
        }

        public Task<ApiResult<TResponse>> Process<TResponse>(IQuery<TResponse> query)
        {
            //  public interface IQuery<out TResponse> : IRequest<TResponse>

            // Task<TResponse> Send<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>
            // TODO I've got the signature here all kinds of borked up. Shouldn't code at 11:00 on sunday night
            var result = this.queryBus.Send<IQuery<TResponse>, TResponse>(query);
        }
    }
}