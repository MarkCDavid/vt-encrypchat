using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vt_encrypchat.Infrastructure.Authentication;
using vt_encrypchat.Infrastructure.MongoDb;

namespace vt_encrypchat.Infrastructure.IoC
{
    public static class Config
    {
        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterConfiguration<MongoDbConfig>(services, configuration, "MongoDB");
            var jwtTokenConfig =
                RegisterConfiguration<JwtTokenConfig, JwtTokenConfig>(services, configuration, "JwtToken");
            
            Configuration.Authentication.ConfigureAuthentication(services, jwtTokenConfig);
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