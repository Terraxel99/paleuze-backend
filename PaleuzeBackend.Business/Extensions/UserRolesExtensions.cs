using PaleuzeBackend.Business.Models.Authentication;

namespace PaleuzeBackend.Business.Extensions
{
    public static class UserRolesExtensions
    {
        public static string ToRoleName(this UserRole role) => role switch
        {
            UserRole.TournamentViewer => "TournamentViewer",
            UserRole.TournamentManager => "TournamentManager",
            UserRole.Admin => "Admin",
            
            _ => throw new ArgumentOutOfRangeException(nameof(role)),
        };
    }
}
