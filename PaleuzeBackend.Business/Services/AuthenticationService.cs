using PaleuzeBackend.Business.Interfaces;
using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Repositories;

namespace PaleuzeBackend.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            this._authenticationRepository = authenticationRepository;
        }

        public async Task RegisterAsync(string username, string password)
        {
            var success = await this._authenticationRepository.RegisterAsync(username, password);

            if (!success)
            {
                throw new InvalidOperationException(); // TODO: Custom exception.
            }
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var user = await this._authenticationRepository.GetUserByUsernameAsync(username);

            if (user is null || !user.Roles.Any())
            {
                throw new InvalidOperationException(); // TODO: Custom exception.
            }

            var result = await this._authenticationRepository.LoginAsync(username, password);

            switch (result)
            {
                case LoginStatus.Failure:
                    throw new InvalidOperationException(); // TODO: Custom exception.

                case LoginStatus.LockedOut:
                    throw new InvalidOperationException(); // TODO: Custom exception.
            }

            return user;
        }

        public async Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
