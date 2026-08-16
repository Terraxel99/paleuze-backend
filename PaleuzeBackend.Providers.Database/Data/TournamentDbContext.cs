using Microsoft.EntityFrameworkCore;

using PaleuzeBackend.Providers.Database.Entities;

namespace PaleuzeBackend.Providers.Database
{
    public class TournamentDbContext : DbContext
    {
        public TournamentDbContext(DbContextOptions<TournamentDbContext> options)
            : base(options)
        { }

        public DbSet<TournamentEntity> Tournaments => Set<TournamentEntity>();
    }
}
