using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PaleuzeBackend.Api.Authentication;
using PaleuzeBackend.Api.Models;
using PaleuzeBackend.Business.Interfaces;

namespace PaleuzeBackend.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly TokenGenerator _tokenGenerator;
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

        [HttpPost("me")]
        [Authorize]
        public async Task<ActionResult<UserResponse>> Me()
        {
            return this.NotFound();
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

            var token = this._tokenGenerator.GenerateToken(new TokenUser { UserName = authenticatedUser.UserName, Roles = authenticatedUser.Roles });

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
