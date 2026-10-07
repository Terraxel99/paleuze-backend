using Microsoft.AspNetCore.Identity;

using PaleuzeBackend.Api.Mapping;

using PaleuzeBackend.Business.Interfaces;
using PaleuzeBackend.Business.Services;

using PaleuzeBackend.Providers.Database;
using PaleuzeBackend.Providers.Database.Mapping;
using PaleuzeBackend.Providers.Security.Configuration;

namespace PaleuzeBackend.Api.Configuration
{
    public static class DependencyInjection
    {
        private const string DB_CONNSTRING_CONFIG_KEY = "Database";
        private const string AUTOMAPPER_LICENSE_CONFIG_KEY = "Automapper.License";
        private const string ASPNET_IDENTITY_CONFIG_KEY = "Identity";
        


        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<ISerieService, SerieService>();

            return services;
        }

        public static IServiceCollection AddDatabaseProvider(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IdentityOptions>(configuration.GetSection(ASPNET_IDENTITY_CONFIG_KEY));

            var connectionString = configuration.GetConnectionString(DB_CONNSTRING_CONFIG_KEY);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException($"Empty connection string \"{DB_CONNSTRING_CONFIG_KEY}\" is not allowed.");
            }

            services.AddDatabaseProvider(connectionString);
            services.AddRepositories();

            return services;
        }

        public static IServiceCollection AddSecurityProvider(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSecurity(configuration);
            return services;
        }

        public static IServiceCollection AddModelMapping(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(config =>
            {
                config.LicenseKey = configuration.GetValue<string>(AUTOMAPPER_LICENSE_CONFIG_KEY);
                config.AddProfile<ApiMappingProfile>();
                config.AddProfile<DatabaseMappingProfile>();
            });

            return services;
        }
    }
}