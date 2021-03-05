using Microsoft.Extensions.DependencyInjection;
using vt_encrypchat.Infrastructure.Authentication;

namespace vt_encrypchat.Infrastructure.IoC
{
    public static class Authentication
    {
        public static void AddAuthentication(this IServiceCollection services)
        {
            services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
        }
    }
}