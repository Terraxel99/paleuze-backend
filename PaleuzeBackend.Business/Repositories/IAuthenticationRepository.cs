using PaleuzeBackend.Business.Models;

namespace PaleuzeBackend.Business.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<LoginStatus> LoginAsync(string username, string password);
        Task<bool> RegisterAsync(string username, string password);
    }
}
