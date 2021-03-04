using Microsoft.Extensions.DependencyInjection;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Data.Repository;

namespace vt_encrypchat.IoC
{
    public static class Repository
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}