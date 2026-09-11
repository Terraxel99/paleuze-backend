using Microsoft.EntityFrameworkCore;
using PaleuzeBackend.Providers.Database.Entities;

namespace PaleuzeBackend.Providers.Database.Data
{
    internal static class DatabaseChecks
    {
        internal static void CheckRefreshTokenConstraints(this ModelBuilder builder)
        {
            builder.Entity<RefreshTokenEntity>(entity =>
            {
                entity.HasIndex(x => x.TokenHash)
                    .IsUnique();
            });
        }
    }
}
