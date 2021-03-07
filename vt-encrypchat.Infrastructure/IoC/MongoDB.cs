using Microsoft.Extensions.DependencyInjection;
using vt_encrypchat.Data.Contracts.MongoDB;
using vt_encrypchat.Data.MongoDB;

namespace vt_encrypchat.Infrastructure.IoC
{
    public static class MongoDb
    {
        public static void AddMongoDb(this IServiceCollection services)
        {
            services.AddSingleton<IMongoContext, MongoContext>();
        }
    }
}