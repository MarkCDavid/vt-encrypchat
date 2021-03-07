using MongoDB.Driver;

namespace vt_encrypchat.Data.Contracts.MongoDB
{
    public interface IMongoContext
    {
        MongoClient Client { get; }
        IMongoDatabase DefaultDatabase { get; }
    }
}