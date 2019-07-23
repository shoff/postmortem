using MongoDB.Driver;

namespace PostMortem.Data.MongoDb
{
    public interface IMongoDbContext
    {
        IMongoClient Client { get; }
        IMongoDatabase Database { get; }
    }
}