using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using PaleuzeBackend.Providers.Database.Entities;

namespace PaleuzeBackend.Providers.Database.Data
{
    public class TournamentDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
    {
        public TournamentDbContext(DbContextOptions<TournamentDbContext> options)
            : base(options)
        { }

        public DbSet<RefreshTokenEntity> RefreshTokens => Set<RefreshTokenEntity>();
        public DbSet<TournamentEntity> Tournaments => Set<TournamentEntity>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.EnsureRolesInDatabase();
            builder.CheckRefreshTokenConstraints();
        }
    }
}
