using Microsoft.Extensions.Options;
using MongoDB.Driver;
using vt_encrypchat.Data.Contracts.MongoDB;
using vt_encrypchat.Infrastructure.Configuration.Model;

namespace vt_encrypchat.Data.MongoDB
{
    public class MongoContext: IMongoContext
    {
        
        public MongoContext(IOptionsMonitor<MongoDbConfig> mongoOptionsMonitor)
        {
            MongoDbConfig mongoDbConfig = mongoOptionsMonitor.CurrentValue;
            
            Client = new MongoClient(mongoDbConfig.ConnectionString);
            DefaultDatabase = Client.GetDatabase(mongoDbConfig.DefaultDatabase);
        }
        
        public MongoClient Client { get; }
        public IMongoDatabase DefaultDatabase { get; }
    }
}