using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Models.Authentication;

namespace PaleuzeBackend.Business.Interfaces
{
    public interface IAuthenticationService
    {
        Task RegisterAsync(string username, string password);
        Task<UserToken> LoginAsync(string username, string password);
        Task LogoutAsync();
        Task RefreshAsync(string refreshToken);
        Task ApproveUserAsync(Guid userId);
        Task<IEnumerable<User>> GetPendingApprovalUsersAsync();
    }
}
