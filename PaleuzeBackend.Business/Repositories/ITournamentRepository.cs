using PaleuzeBackend.Business.Models;

namespace PaleuzeBackend.Business.Repositories
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<Tournament>> GetAllAsync();
        Task<Tournament> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(Tournament tournament);
        Task UpdateAsync(Guid id, Tournament tournament);
        Task DeleteAsync(Guid id);
    }
}