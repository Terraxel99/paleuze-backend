using PaleuzeBackend.Business.Exceptions.General;
using PaleuzeBackend.Business.Interfaces;
using PaleuzeBackend.Business.Models.Organisations;
using PaleuzeBackend.Business.Repositories;

namespace PaleuzeBackend.Business.Services
{
    public class OrganisationsService : IOrganisationsService
    {
        private readonly ICurrentUser _currentUser;
        private readonly IOrganisationsRepository _organisationsRepository;

        public OrganisationsService(
            ICurrentUser currentUser,
            IOrganisationsRepository organisationsRepository)
        {
            this._currentUser = currentUser;
            this._organisationsRepository = organisationsRepository;
        }

        public async Task<IEnumerable<Organisation>> GetAllAsync()
        {
            return await this._organisationsRepository.GetAllAsync();
        }

        public async Task<Organisation> GetByIdAsync(Guid id)
        {
            var organisation = await this._organisationsRepository.GetByIdAsync(id);

            // Requires being manager of organisation OR Admin role.
            if (this._currentUser.Id != organisation.ManagerUserId || !this._currentUser.IsAdmin)
            {
                throw new InsufficientPermissionsException(this._currentUser.Id, $"Get organisation {organisation.Id}");
            }

            return organisation;
        }

        public Task<Guid> CreateAsync(Organisation organisation)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, Organisation organisation)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task AddUserToOrganisationAsync(Guid organisationId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
