using PaleuzeBackend.Business.Models;

namespace PaleuzeBackend.Business.Interfaces
{
    public interface ITournamentService
    {
        Task<IEnumerable<Tournament>> GetAll();
        Task<Tournament> GetById(Guid id);
        Task<Guid> Create(Tournament tournament);
        Task Update(Guid id, Tournament tournament);
        Task Delete(Guid id);
    }
}