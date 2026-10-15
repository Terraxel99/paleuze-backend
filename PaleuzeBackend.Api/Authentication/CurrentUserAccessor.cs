using System.Security.Claims;

using PaleuzeBackend.Business.Interfaces;
using PaleuzeBackend.Business.Models.Authentication;

namespace PaleuzeBackend.Api.Authentication
{
    public class CurrentUserAccessor : ICurrentUser
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserAccessor(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }

        public Guid? Id
        {
            get
            {
                var value = this.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return Guid.TryParse(value, out var id) ? id : null;
            }
        }

        public string? Name
            => this.User?.FindFirstValue(ClaimTypes.Name);

        public bool IsAuthenticated 
            => this.User?.Identity?.IsAuthenticated == true;

        public bool IsAdmin
            => this.IsInRole(UserRole.Admin);

        public bool IsInRole(string role)
            => this.User?.IsInRole(role) == true;


        private ClaimsPrincipal? User
            => this._contextAccessor.HttpContext?.User;
    }
}
