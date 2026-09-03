using PaleuzeBackend.Business.Models;

namespace PaleuzeBackend.Business.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> LoginAsync(string username, string password);
        Task RegisterAsync(string username, string password);
        Task LogoutAsync();
    }
}
