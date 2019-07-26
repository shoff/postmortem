using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostMortem.Infrastructure.Events
{
    public interface IEventStoreRepository<TEntity, in TEntityId,TEventArgs>
        where TEventArgs : class,IEventArgs
        where TEntity : IEventsEntity<TEntityId,TEventArgs>,new()
        where TEntityId : Infrastructure.IEntityId
    {
        Task SaveAsync(TEntity entity);
        TEntity GetById(TEntityId id);
        IEnumerable<TEventArgs> LoadEvents(TEntityId id);
        Task DeleteByIdAsync(TEntityId id);
    }
}