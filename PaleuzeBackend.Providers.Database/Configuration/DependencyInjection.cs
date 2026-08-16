using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using PaleuzeBackend.Business.Repositories;
using PaleuzeBackend.Providers.Database.Repositories;

namespace PaleuzeBackend.Providers.Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<TournamentDbContext>(options =>
            {
               options.UseNpgsql(connectionString); 
            });
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITournamentRepository, TournamentRepository>();

            return services;
        }
    }
}