using PaleuzeBackend.Business.Models;

namespace PaleuzeBackend.Business.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<LoginStatus> LoginAsync(string username, string password);
        Task LogoutAsync(string userId);
        Task CreateRefreshTokenAsync(Guid userId, string hashedRefreshToken, DateTime expiryDate, DateTime maxCumulatedExpiry);
        Task RotateRefreshTokenAsync(string hashedRefreshToken);
        Task<bool> RegisterAsync(string username, string password);
        Task<bool> ApproveUserAsync(Guid userId);
        Task<IEnumerable<User>> GetPendingApprovalUsersAsync();
    }
}
