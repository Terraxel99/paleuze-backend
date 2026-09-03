using PaleuzeBackend.Api.Mapping;

using PaleuzeBackend.Business.Interfaces;
using PaleuzeBackend.Business.Services;

using PaleuzeBackend.Providers.Database;
using PaleuzeBackend.Providers.Database.Mapping;

namespace PaleuzeBackend.Api.Configuration
{
    public static class DependencyInjection
    {
        private const string DB_CONNSTRING = "Database";

        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITournamentService, TournamentService>();

            return services;
        }

        public static IServiceCollection AddDatabaseProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(DB_CONNSTRING);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException($"Empty connection string \"{DB_CONNSTRING}\" is not allowed.");
            }

            services.AddDatabaseProvider(connectionString);
            services.AddRepositories();

            return services;
        }

        public static IServiceCollection AddModelMapping(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(config =>
            {
                // TODO : config.LicenseKey = configuration.Get("Automapper.License");
                config.AddProfile<ApiMappingProfile>();
                config.AddProfile<DatabaseMappingProfile>();
            });

            return services;
        }
    }
}