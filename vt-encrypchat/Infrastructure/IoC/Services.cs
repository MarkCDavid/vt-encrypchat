using Microsoft.Extensions.DependencyInjection;
using vt_encrypchat.Services;
using vt_encrypchat.Services.Contracts;

namespace vt_encrypchat.Infrastructure.IoC
{
    public static class Services
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}