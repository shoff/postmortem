namespace PostMortem.Domain
{
    using System.Net;
    using System.Threading.Tasks;
    using Zatoichi.Common.Infrastructure.Services;
    using Zatoichi.EventSourcing.Commands;
    using Zatoichi.EventSourcing.Queries;

    // TODO this should ONLY be used to send messages to an message queue like kafka
    public class EventBus : IEventBus
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

        public Task Process(ICommand command)
        {
            return this.commandBus.Send(command);
        }

        public async Task<ApiResult<TResponse>> Process<TResponse>(IQuery<TResponse> query)
        {
            //  public interface IQuery<out TResponse> : IRequest<TResponse>

            // Task<TResponse> Send<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>
            // TODO I've got the signature here all kinds of borked up. Shouldn't code at 11:00 on sunday night
            var result = await this.queryBus.Send<IQuery<TResponse>, TResponse>(query);
            return new ApiResult<TResponse>(HttpStatusCode.OK, result);
        }
    }
}