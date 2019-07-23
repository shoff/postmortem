using PostMortem.Domain.EventSourcing.Events;

namespace PostMortem.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Comments;
    using Projects;
    using Questions;

    public interface IRepository<TEntity, in TEntityId>
        where TEntityId :IEntityId
        where TEntity : IEntity<TEntityId>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(TEntityId id);

        Task SaveAsync(TEntity entity);
        //void Delete(TEntity entity);
        //void DeleteById(TEntityId commentId);
    }

    
}