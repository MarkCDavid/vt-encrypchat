using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vt_encrypchat.Infrastructure.Configuration.Model;

namespace vt_encrypchat.Infrastructure.Configuration
{
    public static class Config
    {
        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterConfiguration<MongoDbConfig>(services, configuration, "MongoDB");
            Authentication.ConfigureAuthentication(services);
        }


        private static IConfigurationSection RegisterConfiguration<TConfig>
            (IServiceCollection services, IConfiguration configuration, string sectionName)
            where TConfig : class
        {
            var section = configuration.GetSection(sectionName);
            services.Configure<TConfig>(section);
            return section;
        }

        private static TBind RegisterConfiguration<TConfig, TBind>(IServiceCollection services,
            IConfiguration configuration, string sectionName)
            where TConfig : class where TBind : new()
        {
            var bind = new TBind();
            RegisterConfiguration<TConfig>(services, configuration, sectionName).Bind(bind);
            return bind;
        }
    }
}