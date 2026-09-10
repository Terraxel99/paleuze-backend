using PaleuzeBackend.Business.Exceptions.Authentication;
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
                throw new UserAlreadyExistsException(username);
            }
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var user = await this._authenticationRepository.GetUserByUsernameAsync(username);

            if (user is null || !user.Roles.Any())
            {
                throw new UserNotFoundException(username);
            }

            var result = await this._authenticationRepository.LoginAsync(username, password);

            switch (result)
            {
                case LoginStatus.Failure:
                    throw new InvalidCredentialsException();

                case LoginStatus.LockedOut:
                    throw new UserLockedOutException(username);
            }

            return user;
        }

        public async Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task ApproveUserAsync(Guid userId)
        {
            var success = await this._authenticationRepository.ApproveUserAsync(userId);

            if (!success)
            {
                throw new UserNotFoundException(userId);
            }
        }

        public async Task<IEnumerable<User>> GetPendingApprovalUsersAsync()
            => await this._authenticationRepository.GetPendingApprovalUsersAsync();
    }
}
