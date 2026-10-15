using PaleuzeBackend.Business.Models.Organisations;

namespace PaleuzeBackend.Business.Interfaces
{
    public interface IOrganisationsService
    {
        Task<IEnumerable<Organisation>> GetAllAsync();
        Task<Organisation> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(Organisation organisation);
        Task UpdateAsync(Guid id, Organisation organisation);
        Task DeleteAsync(Guid id);
        Task AddUserToOrganisationAsync(Guid organisationId, Guid userId);
    }
}
