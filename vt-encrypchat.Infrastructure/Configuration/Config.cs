using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vt_encrypchat.Data.Configuration;

namespace vt_encrypchat.Infrastructure.Configuration
{
    public static class Config
    {
        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbConfig>(configuration.GetSection("MongoDB"));
            Authentication.ConfigureAuthentication(services);
        }
    }
}