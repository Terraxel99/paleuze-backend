using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Models.Authentication;
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

            var model = this._mapper.Map<User>(user);
            model.Roles = roles;
            
            return model;
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            // User is not approved by default and needs to be approved by admin later.
            var userEntity = new UserEntity
            { 
                Id = Guid.NewGuid(), 
                UserName = username,
                IsApproved = false 
            };

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

            await this._userManager.ResetAccessFailedCountAsync(user); // Resets counter of attempts to 0.
            return LoginStatus.Success;
        }

        public async Task RevokeRefreshTokenAsync(string hashedRefreshToken)
        {
            var refreshToken = await this._database.RefreshTokens
                .SingleOrDefaultAsync(rt => rt.TokenHash == hashedRefreshToken);

            if (refreshToken is null)
            {
                throw new InvalidOperationException();
            } 

            refreshToken.RevokedAt = DateTime.UtcNow;
            await this._database.SaveChangesAsync();
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


            IdentityResult queryResult;
            user.IsApproved = true;
            
            await using var transaction = await this._database.Database.BeginTransactionAsync();

            queryResult = await this._userManager.UpdateAsync(user);

            if (!queryResult.Succeeded)
            {
                return false;
            }

            // TODO: Adapt this in the future so that we can choose which roles to add.
            queryResult = await this._userManager.AddToRoleAsync(user, UserRole.TournamentManager);

            if (!queryResult.Succeeded)
            {
                return false;
            }

            await transaction.CommitAsync();
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

        public async Task<Guid> CreateRefreshTokenAsync(RefreshToken refreshToken)
        {
            var entity = this._mapper.Map<RefreshTokenEntity>(refreshToken);

            await this._database.RefreshTokens.AddAsync(entity);
            await this._database.SaveChangesAsync();

            return entity.Id;
        }

        public async Task RotateRefreshTokenAsync(RefreshToken newToken)
        {
            await using var transaction = await this._database.Database.BeginTransactionAsync();

            var newTokenId = await this.CreateRefreshTokenAsync(newToken);

            await this._database.RefreshTokens
                .Where(rt => rt.UserId == newToken.UserId && rt.RevokedAt == null)
                .ExecuteUpdateAsync(rt =>
                {
                   rt.SetProperty(r => r.RevokedAt, DateTime.UtcNow);
                   rt.SetProperty(r => r.ReplacedByTokenId, newTokenId);
                });

            await transaction.CommitAsync();
        }

        public async Task<User?> GetUserByValidRefreshTokenAsync(string hashedRefreshToken)
        {
            var refreshToken = await this._database.RefreshTokens
                .Include(rt => rt.User)
                .SingleOrDefaultAsync(rt => rt.TokenHash == hashedRefreshToken);

            if (refreshToken is null)
            {
                return default;
            }
            
            var model = this._mapper.Map<User>(refreshToken.User);
            model.Roles = await this._userManager.GetRolesAsync(refreshToken.User);

            return model;
        }
    }
}
