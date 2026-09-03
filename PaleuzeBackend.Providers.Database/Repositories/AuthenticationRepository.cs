using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaleuzeBackend.Business.Extensions;
using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Models.Authentication;
using PaleuzeBackend.Business.Repositories;

using PaleuzeBackend.Providers.Database.Entities;

namespace PaleuzeBackend.Providers.Database.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<UserEntity> _userManager;

        public AuthenticationRepository(UserManager<UserEntity> userManager)
        {
            this._userManager = userManager;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            var user = await this._userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
            
            if (user is null)
            {
                return null;
            }

            var roles = await this._userManager.GetRolesAsync(user);

            return new User
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                Roles = roles,
            };
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            var existing = await this._userManager.Users.AnyAsync(u => u.UserName == username);

            if (existing)
            {
                return false;
            }

            var userEntity = new UserEntity { UserName = username };
            var result = await this._userManager.CreateAsync(userEntity, password);

            // TODO: Change, no role set by default and Admin gives roles to people by specific endpoint.
            await this._userManager.AddToRoleAsync(userEntity, UserRole.Admin.ToRoleName()); 

            return result.Succeeded;
        }

        public async Task<LoginStatus> LoginAsync(string username, string password)
        {
            var user = await this._userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user is null)
            {
                return LoginStatus.Failure;
            }

            if (await this._userManager.IsLockedOutAsync(user))
            {
                return LoginStatus.LockedOut;
            }

            var success = await this._userManager.CheckPasswordAsync(user, password);

            if (!success)
            {
                await this._userManager.AccessFailedAsync(user); // Increments failure counter to lock out the user if too much attempts.

                return await this._userManager.IsLockedOutAsync(user) ?
                    LoginStatus.LockedOut :
                    LoginStatus.Failure;
            }

            await this._userManager.ResetAccessFailedCountAsync(user); // Resets counter of attempts to 0.
            return LoginStatus.Success;
        }
    }
}
