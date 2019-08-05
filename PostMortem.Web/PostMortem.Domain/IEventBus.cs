namespace PostMortem.Domain
{
    using System.Threading.Tasks;
    using Zatoichi.Common.Infrastructure.Services;
    using Zatoichi.EventSourcing.Commands;
    using Zatoichi.EventSourcing.Queries;

    public interface IEventBus
    {
        Task Process(ICommand command);
        Task<ApiResult<TResponse>> Process<TResponse>(IQuery<TResponse> query);
    }
}