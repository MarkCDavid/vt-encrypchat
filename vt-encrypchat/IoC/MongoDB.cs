using Microsoft.Extensions.DependencyInjection;
using vt_encrypchat.Data.Contracts.MongoDB;
using vt_encrypchat.Data.MongoDB;

namespace vt_encrypchat.IoC
{
    public static class MongoDB
    {
        public static void AddMongoDB(this IServiceCollection services)
        {
            services.AddSingleton<IMongoContext, MongoContext>();
        }
    }
}