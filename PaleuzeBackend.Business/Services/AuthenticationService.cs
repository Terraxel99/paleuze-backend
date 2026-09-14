using Microsoft.Extensions.Options;

using PaleuzeBackend.Business.Exceptions.Authentication;
using PaleuzeBackend.Business.Interfaces;
using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Models.Authentication;
using PaleuzeBackend.Business.Repositories;
using PaleuzeBackend.Business.Security;

namespace PaleuzeBackend.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IHashingRepository _hashingRepository;
        private readonly RefreshTokensSettings _refreshTokenSettings;

        public AuthenticationService(
            IAuthenticationRepository authenticationRepository,
            ITokenRepository tokenRepository,
            IHashingRepository hashingRepository,
            IOptions<RefreshTokensSettings> refreshTokenSettings)
        {
            this._authenticationRepository = authenticationRepository;
            this._tokenRepository = tokenRepository;
            this._hashingRepository = hashingRepository;
            this._refreshTokenSettings = refreshTokenSettings.Value;
        }

        public async Task RegisterAsync(string username, string password)
        {
            var success = await this._authenticationRepository.RegisterAsync(username, password);

            if (!success)
            {
                throw new UserAlreadyExistsException(username);
            }
        }

        public async Task<UserToken> LoginAsync(string username, string password)
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
                    throw new UserLockedOutException(user);

                case LoginStatus.NotApproved:
                    throw new UserNotApprovedException(user);
            }

            var accessToken = this._tokenRepository.SignAccessToken(user);
            var refreshToken = await this._tokenRepository.GenerateRandomRefreshTokenAsync(user.Id);

            await this._authenticationRepository.CreateRefreshTokenAsync(refreshToken);

            return new UserToken
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiry = refreshToken.ExpiresAt,
                User = user
            };
        }

        public async Task LogoutAsync()
        {
            throw new NotImplementedException(); // TODO : Handle refresh active token(s).
        }

        public async Task<UserToken> RefreshAsync(string refreshToken)
        {
            var oldRefreshTokenHashed = await this._hashingRepository.SHA256HashAsync(refreshToken);
            var user = await this._authenticationRepository.GetUserByValidRefreshTokenAsync(oldRefreshTokenHashed);

            if (user is null)
            {
                throw new InvalidRefreshTokenException(refreshToken);
            }

            if (!user.IsApproved)
            {
                throw new UserNotApprovedException(user);
            }

            var newAccessToken = this._tokenRepository.SignAccessToken(user);
            var newRefreshToken = await this._tokenRepository.GenerateRandomRefreshTokenAsync(user.Id);

            await this._authenticationRepository.RotateRefreshTokenAsync(newRefreshToken);

            // TODO :
            // 1 - Model could be better with refreshtoken and expiry in single object
            // 2 - Sliding and total expiry ?
            return new UserToken
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiry = newRefreshToken.ExpiresAt, 
                User = user,
            };
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
