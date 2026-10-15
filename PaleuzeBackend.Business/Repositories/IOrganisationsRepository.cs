using PaleuzeBackend.Business.Models.Organisations;

namespace PaleuzeBackend.Business.Repositories
{
    public interface IOrganisationsRepository
    {
        Task<IEnumerable<Organisation>> GetAllAsync();
        Task<Organisation> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(Organisation organisation);
        Task UpdateAsync(Guid organisationId, Organisation organisation);
        Task DeleteAsync(Guid id);
    }
}
