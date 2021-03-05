using Microsoft.Extensions.DependencyInjection;

namespace vt_encrypchat.Infrastructure.IoC
{
    public static class IoC
    {
        public static void AddIoCMappings(this IServiceCollection services)
        {
            services.AddMongoDB();
            services.AddRepository();
        }
    }
}