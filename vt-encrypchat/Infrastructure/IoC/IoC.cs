using Microsoft.Extensions.DependencyInjection;
using vt_encrypchat.Infrastructure.Configuration;

namespace vt_encrypchat.Infrastructure.IoC
{
    public static class IoC
    {
        public static void AddIoCMappings(this IServiceCollection services)
        {
            services.AddMongoDB();
            services.AddRepository();
            services.AddAuthentication();
            services.AddServices();
        }
    }
}