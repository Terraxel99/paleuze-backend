using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Repositories;
using PaleuzeBackend.Business.Security;

namespace PaleuzeBackend.Providers.Security.Services.Tokens
{
    public class TokenProvider : ITokenRepository
    {
        private const int REFRESH_TOKEN_BYTES_LENGTH = 64;


        private readonly JwtSettings _jwtSettings;

        public TokenProvider(IOptions<JwtSettings> settings)
            => this._jwtSettings = settings.Value;

        public string GenerateAccessToken(User user)
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

        public string GenerateRandomRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(REFRESH_TOKEN_BYTES_LENGTH);
            return Convert.ToBase64String(bytes);
        }
    }
}
