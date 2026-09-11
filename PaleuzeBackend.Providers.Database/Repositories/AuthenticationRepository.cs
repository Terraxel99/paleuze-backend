using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Repositories;
using PaleuzeBackend.Providers.Database.Data;
using PaleuzeBackend.Providers.Database.Entities;

namespace PaleuzeBackend.Providers.Database.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly TournamentDbContext _database;
        private readonly IMapper _mapper;

        public AuthenticationRepository(
            UserManager<UserEntity> userManager,
            TournamentDbContext dbContext,
            IMapper mapper)
        {
            this._userManager = userManager;
            this._database = dbContext;
            this._mapper = mapper;
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

            // User is not approved by default and needs to be approved by admin later.
            var userEntity = new UserEntity { UserName = username, IsApproved = false };
            var result = await this._userManager.CreateAsync(userEntity, password);

            return result.Succeeded;
        }

        public async Task<LoginStatus> LoginAsync(string username, string password)
        {
            var user = await this._userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user is null)
            {
                return LoginStatus.Failure;
            }

            if (!user.IsApproved)
            {
                return LoginStatus.NotApproved;
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

            // Generate token
            // Generate refresh token and store it in db.

            await this._userManager.ResetAccessFailedCountAsync(user); // Resets counter of attempts to 0.
            return LoginStatus.Success;
        }

        public async Task LogoutAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ApproveUserAsync(Guid userId)
        {
            var user = await this._userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
            {
                return false;
            }

            if (user.IsApproved)
            {
                return true;
            }

            user.IsApproved = true;
            await this._userManager.UpdateAsync(user);

            return true;
        }

        public async Task<IEnumerable<User>> GetPendingApprovalUsersAsync()
        {
            var users = await this._userManager.Users
                .AsNoTracking()
                .Where(u => !u.IsApproved)
                .ToListAsync();

            return this._mapper.Map<IEnumerable<User>>(users);
        }

        public async Task CreateRefreshTokenAsync(Guid userId, string hashedRefreshToken, DateTime expiryDate, DateTime maxCumulatedExpiry)
        {
            var entity = new RefreshTokenEntity
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                TokenHash = hashedRefreshToken,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = expiryDate,
                AbsoluteExpiresAt = maxCumulatedExpiry,
            };

            await this._database.RefreshTokens.AddAsync(entity);
            await this._database.SaveChangesAsync();
        }

        public async Task RotateRefreshTokenAsync(string hashedRefreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
