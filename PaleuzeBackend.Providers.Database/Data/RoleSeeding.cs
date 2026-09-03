using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PaleuzeBackend.Providers.Database.Data
{
    internal static class RoleSeeding
    {
        internal static void EnsureRolesInDatabase(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>
                {
                    Id = new Guid("dcff469b-c71a-4bb1-b7b7-8352d2c23b14"),
                    Name = "TournamentViewer",
                    NormalizedName = "TournamentViewer",
                    ConcurrencyStamp = "TournamentViewer",
                },
                new IdentityRole<Guid>
                {
                    Id = new Guid("68f33d13-2d34-40ca-a3cd-460c4fc5e7a8"),
                    Name = "TournamentManager",
                    NormalizedName = "TournamentManager",
                    ConcurrencyStamp = "TournamentManager",
                },
                new IdentityRole<Guid>
                {
                    Id = new Guid("46db37fb-1894-4122-a782-2d75aafc1bd1"),
                    Name = "Admin",
                    NormalizedName = "Admin",
                    ConcurrencyStamp = "Admin",
                }
            );
        }
    }
}
