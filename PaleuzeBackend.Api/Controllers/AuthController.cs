using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PaleuzeBackend.Api.Authentication;
using PaleuzeBackend.Api.Models;
using PaleuzeBackend.Business.Extensions;
using PaleuzeBackend.Business.Interfaces;
using PaleuzeBackend.Business.Models.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace PaleuzeBackend.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly IAuthenticationService _authenticationService;

        private readonly IMapper _mapper;

        public AuthController(
            IAuthenticationService authenticationService,
            TokenGenerator tokenGenerator,
            IMapper mapper)
        {
            this._authenticationService = authenticationService;
            this._tokenGenerator = tokenGenerator;
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
            var authenticatedUser = this._mapper.Map<UserResponse>(
                await this._authenticationService.LoginAsync(data.UserName, data.Password));

            var token = this._tokenGenerator.GenerateToken(new TokenUser { Id = authenticatedUser.Id, UserName = authenticatedUser.UserName, Roles = authenticatedUser.Roles });

            return this.Ok(new LoginResponse { Token = token, User = authenticatedUser });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            return this.NotFound();
        }
    }
}
