using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using System.Text;

using PaleuzeBackend.Business.Repositories;
using PaleuzeBackend.Providers.Security.Services.Tokens;
using PaleuzeBackend.Providers.Security.Services.Hashing;
using PaleuzeBackend.Business.Security;

namespace PaleuzeBackend.Providers.Security.Configuration
{
    public static class Configure
    {
        private const string JWT_CONFIG_SECTION_NAME = "Jwt";
        private const string REFRESHTOKENS_CONFIG_SECTION_NAME = "RefreshTokens";


        public static void AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JWT_CONFIG_SECTION_NAME));
            services.Configure<RefreshTokensSettings>(configuration.GetSection(REFRESHTOKENS_CONFIG_SECTION_NAME));
           
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        var jwt = configuration.GetSection(JWT_CONFIG_SECTION_NAME).Get<JwtSettings>()!;

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwt.Issuer,
                            ValidAudience = jwt.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret))
                        };
                    });

            // Add exposed services/repos :
            services.AddScoped<ITokenRepository, TokenProvider>();
            services.AddScoped<IHashingRepository, HashingRepository>();
        }
    }
}
