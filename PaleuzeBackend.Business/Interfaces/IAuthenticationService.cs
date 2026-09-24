using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Models.Authentication;

namespace PaleuzeBackend.Business.Interfaces
{
    public interface IAuthenticationService
    {
        Task RegisterAsync(string username, string password);
        Task<UserToken> LoginAsync(string username, string password);
        Task LogoutAsync(string refreshToken);
        Task<UserToken> RefreshAsync(string refreshToken);
        Task ApproveUserAsync(Guid userId);
        Task<IEnumerable<User>> GetPendingApprovalUsersAsync();
    }
}
