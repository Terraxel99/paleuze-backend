using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Models.Authentication;

namespace PaleuzeBackend.Business.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<LoginStatus> LoginAsync(string username, string password);
        Task LogoutAsync(string userId);
        Task<Guid> CreateRefreshTokenAsync(RefreshToken refreshToken);
        Task RotateRefreshTokenAsync(RefreshToken newToken);
        Task<User?> GetUserByValidRefreshTokenAsync(string hashedRefreshToken);
        Task<bool> RegisterAsync(string username, string password);
        Task<bool> ApproveUserAsync(Guid userId);
        Task<IEnumerable<User>> GetPendingApprovalUsersAsync();
    }
}
