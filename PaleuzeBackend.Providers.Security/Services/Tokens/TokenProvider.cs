using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Repositories;
using PaleuzeBackend.Business.Security;
using PaleuzeBackend.Business.Models.Authentication;

namespace PaleuzeBackend.Providers.Security.Services.Tokens
{
    public class TokenProvider : ITokenRepository
    {
        private const int REFRESH_TOKEN_BYTES_LENGTH = 64;

        private readonly IHashingRepository _hashingRepository;
        private readonly RefreshTokensSettings _refreshTokensSettings;
        private readonly JwtSettings _jwtSettings;

        public TokenProvider(
            IHashingRepository hashingRepository,
            IOptions<RefreshTokensSettings> refreshTokensSettings,
            IOptions<JwtSettings> jwtSettings)
        {
            this._hashingRepository = hashingRepository;
            this._refreshTokensSettings = refreshTokensSettings.Value;
            this._jwtSettings = jwtSettings.Value;
        }

        public string SignAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, user.UserName),
            };

            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<RefreshToken> GenerateRandomRefreshTokenAsync(Guid userId)
        {
            var bytes = RandomNumberGenerator.GetBytes(REFRESH_TOKEN_BYTES_LENGTH);
            var token = Convert.ToBase64String(bytes);

            return new RefreshToken
            {
                UserId = userId,
                Token = token,
                TokenHash = await this._hashingRepository.SHA256HashAsync(token),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(this._refreshTokensSettings.RefreshTokenExpiryDays),
                AbsoluteExpiresAt = DateTime.UtcNow.AddDays(this._refreshTokensSettings.RefreshTokenMaximumCombinedSessionDays),
            };
        }
    }
}
