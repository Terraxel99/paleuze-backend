using PaleuzeBackend.Business.Models;

namespace PaleuzeBackend.Business.Repositories
{
    public interface ITokenRepository
    {
        string GenerateAccessToken(User user);
        string GenerateRandomRefreshToken();
    }
}
