using PaleuzeBackend.Business.Repositories;
using PaleuzeBackend.Business.Models.Organisations;

namespace PaleuzeBackend.Providers.Database.Repositories
{
    public class OrganisationsRepository : IOrganisationsRepository
    {
        public Task<Guid> AddAsync(Organisation organisation)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Organisation>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Organisation> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid organisationId, Organisation organisation)
        {
            throw new NotImplementedException();
        }
    }
}
