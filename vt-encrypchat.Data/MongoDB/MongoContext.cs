using Microsoft.Extensions.Options;
using MongoDB.Driver;
using vt_encrypchat.Data.Configuration;
using vt_encrypchat.Data.Contracts.MongoDB;

namespace vt_encrypchat.Data.MongoDB
{
    public class MongoContext : IMongoContext
    {
        public MongoContext(IOptionsMonitor<MongoDbConfig> mongoOptionsMonitor)
        {
            var mongoDbConfig = mongoOptionsMonitor.CurrentValue;

            Client = new MongoClient(mongoDbConfig.ConnectionString);
            DefaultDatabase = Client.GetDatabase(mongoDbConfig.DefaultDatabase);
        }

        public MongoClient Client { get; }
        public IMongoDatabase DefaultDatabase { get; }
    }
}