using PostMortem.Infrastructure;

namespace PostMortem.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<TEntity, in TEntityId>
        where TEntityId :IEntityId
        where TEntity : IEntity<TEntityId>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(TEntityId id);

        Task SaveAsync(TEntity entity);
        Task DeleteByIdAsync(TEntityId id);
    }

    
}