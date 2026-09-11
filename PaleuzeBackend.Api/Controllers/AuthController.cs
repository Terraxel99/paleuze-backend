using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PaleuzeBackend.Api.Models;

using PaleuzeBackend.Business.Interfaces;
using PaleuzeBackend.Business.Models.Authentication;

namespace PaleuzeBackend.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string REFRESHTOKEN_COOKIE_NAME = "refreshToken";

        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AuthController(
            IAuthenticationService authenticationService,
            IMapper mapper)
        {
            this._authenticationService = authenticationService;
            this._mapper = mapper;
        }

        [HttpGet("pending-approval")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetPendingApprovalUsers()
        {
            var users = this._mapper.Map<IEnumerable<UserResponse>>(await this._authenticationService.GetPendingApprovalUsersAsync());
            return this.Ok(users);
        }

        [HttpPatch]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<ActionResult> ApproveUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return this.BadRequest();
            }

            await this._authenticationService.ApproveUserAsync(userId);
            return this.NoContent();
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody]RegisterRequest request)
        {
            await this._authenticationService.RegisterAsync(request.UserName, request.Password);
            return this.NoContent();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login([FromBody]LoginRequest data)
        {
            var login = await this._authenticationService.LoginAsync(data.UserName, data.Password);
            var user = this._mapper.Map<UserResponse>(login.User);

            this.SetRefreshTokenCookie(login.RefreshToken, login.RefreshTokenExpiry);

            return this.Ok(new LoginResponse { Token = login.AccessToken, User = user });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            return this.NotFound(); // TODO : Implement.
        }

        private void SetRefreshTokenCookie(string refreshToken, DateTime expiry)
        {
            this.Response.Cookies.Append(
                REFRESHTOKEN_COOKIE_NAME,
                refreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = expiry,
                    Path = "/api/auth",
                }
            );
        }
    }
}
