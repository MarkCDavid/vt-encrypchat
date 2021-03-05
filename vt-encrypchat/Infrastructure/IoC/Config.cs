using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vt_encrypchat.Config;

namespace vt_encrypchat.Infrastructure.IoC
{
    public static class Config
    {
        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBConfig>(configuration.GetSection("MongoDB"));
        }
    }
}