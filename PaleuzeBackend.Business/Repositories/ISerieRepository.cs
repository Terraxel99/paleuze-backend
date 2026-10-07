using PaleuzeBackend.Business.Models;

namespace PaleuzeBackend.Business.Repositories
{
    public interface ISerieRepository
    {
        Task<IEnumerable<Serie>> GetAllByTournamentIdAsync(Guid tournamentId);
        Task<Serie> GetByIdAsync(Guid id);
    }
}
