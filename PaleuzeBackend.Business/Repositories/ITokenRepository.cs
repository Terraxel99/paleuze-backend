using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Models.Authentication;

namespace PaleuzeBackend.Business.Repositories
{
    public interface ITokenRepository
    {
        string SignAccessToken(User user);
        Task<RefreshToken> GenerateRandomRefreshTokenAsync(Guid userId);
    }
}
