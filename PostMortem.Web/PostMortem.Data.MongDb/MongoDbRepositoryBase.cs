using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ChaosMonkey.Guards;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using PostMortem.Domain;

namespace PostMortem.Data.MongoDb
{
    public abstract class MongoDbRepositoryBase<TEntity, TEntityId, TDto, TId> : IRepository<TEntity, TEntityId>
        where TId :struct
        where TEntityId : EntityId<TId>
        where TEntity : IEntity<TEntityId>
    {
        protected IMongoDatabase MongoDatabase { get; private set; }
        protected IMapper Mapper { get; private set; }
        private readonly FindOptions<TDto, TEntity> mapToEntity;

        private readonly ILogger<MongoDbRepositoryBase<TEntity, TEntityId, TDto, TId>> logger;

        protected MongoDbRepositoryBase(IMongoDatabase mongoDatabase,
            IMapper mapper,
            ILogger<MongoDbRepositoryBase<TEntity, TEntityId, TDto,TId>> logger,
            string collectionName = null)
        {
            MongoDatabase = Guard.IsNotNull(mongoDatabase, nameof(mongoDatabase));
            this.logger = Guard.IsNotNull(logger,nameof(logger));
            collectionName = string.IsNullOrEmpty(collectionName) ? typeof(TEntity).Name : collectionName;
            this.Mapper = Guard.IsNotNull(mapper, nameof(mapper));
            this.mapToEntity = new FindOptions<TDto, TEntity>
            {
                Projection =
                    new FindExpressionProjectionDefinition<TDto, TEntity>(dto => Mapper.Map<TDto, TEntity>(dto))
            };
            this.Collection = MongoDatabase.GetCollection<TDto>(collectionName);
        }

        protected IMongoCollection<TDto> Collection { get; private set; }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TDto, bool>> predicate)
        {
            // perform the mapping during retrieval from Mongo
            var cursor = await Collection.FindAsync(FilterBy(predicate), mapToEntity);
            return cursor.ToEnumerable().ToList();
        }

        protected static FilterDefinition<TDto> FilterBy(Expression<Func<TDto, bool>> predicate) =>
            new ExpressionFilterDefinition<TDto>(predicate);

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await FindAllAsync(@true=>true);
        }

        public async Task<TEntity> GetByIdAsync(TEntityId id)
        {
            return await Collection.Find(GetIdFilter(id))
                .Project(x => Mapper.Map<TDto, TEntity>(x))
                .SingleAsync();
        }

        public async Task SaveAsync(TEntity entity)
        {
            var repl = Mapper.Map<TEntity, TDto>(entity);
            var replaceOneResult = await Collection.ReplaceOneAsync(
                GetIdFilter(entity.GetEntityId()),
                repl, 
                new UpdateOptions {IsUpsert = true,});
        }

        public async Task DeleteByIdAsync(TEntityId id)
        {
            await Collection.DeleteOneAsync(GetIdFilter(id));
        }

        protected virtual FilterDefinition<TDto> GetIdFilter(TEntityId id) => Builders<TDto>.Filter.Eq("_id", id.Id);

    }
}