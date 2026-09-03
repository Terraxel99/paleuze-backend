using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using PaleuzeBackend.Business.Repositories;

using PaleuzeBackend.Providers.Database.Data;
using PaleuzeBackend.Providers.Database.Entities;
using PaleuzeBackend.Providers.Database.Repositories;

namespace PaleuzeBackend.Providers.Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabaseProvider(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TournamentDbContext>(options =>
            {
               options.UseNpgsql(connectionString); 
            });

            services.AddIdentityCore<UserEntity>()
                    .AddRoles<IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<TournamentDbContext>();
                    // .AddDefaultTokenProviders(); // TODO : Add password reset & MFA.

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<ITournamentRepository, TournamentRepository>();

            return services;
        }
    }
}