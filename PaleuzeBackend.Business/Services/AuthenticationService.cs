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
                    throw new UserLockedOutException(username);
            }

            var accessToken = this._tokenRepository.GenerateAccessToken(user);
            
            var refreshToken = this._tokenRepository.GenerateRandomRefreshToken();
            var hashedRefreshToken = await this._hashingRepository.SHA256HashAsync(refreshToken);
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(this._refreshTokenSettings.RefreshTokenExpiryDays);
            var refreshTokenMaxCumulatedExpiry = DateTime.UtcNow.AddDays(this._refreshTokenSettings.RefreshTokenMaximumCombinedSessionDays);

            await this._authenticationRepository.CreateRefreshTokenAsync(user.Id, hashedRefreshToken, refreshTokenExpiry, refreshTokenMaxCumulatedExpiry);

            return new UserToken 
            { 
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RefreshTokenExpiry = refreshTokenExpiry,
                User = user
            };
        }

        public async Task LogoutAsync()
        {
            throw new NotImplementedException(); // TODO : Handle refresh active token(s).
        }

        public async Task RefreshAsync(string refreshToken)
        {
            // Check if refresh token is still valid.
            // If not, then : ça dégage.

            // If valid
            // We generate a new access token.
            // We ROTATE refresh token with a newly generated one. (provider hashes! )
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
