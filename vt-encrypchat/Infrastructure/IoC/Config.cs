using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vt_encrypchat.Infrastructure.MongoDb;

namespace vt_encrypchat.Infrastructure.IoC
{
    public static class Config
    {
        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbConfig>(configuration.GetSection("MongoDB"));
        }
    }
}