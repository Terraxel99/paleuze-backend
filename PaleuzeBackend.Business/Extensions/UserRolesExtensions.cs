using PaleuzeBackend.Business.Models.Authentication;

namespace PaleuzeBackend.Business.Extensions
{
    public static class UserRolesExtensions
    {
        public static string ToRoleName(this UserRole role) => role switch
        {
            UserRole.TournamentViewer => "Tournament.Viewer",
            UserRole.TournamentManager => "Tournament.Manager",
            UserRole.Admin => "Admin",
            
            _ => throw new ArgumentOutOfRangeException(nameof(role)),
        };
    }
}
