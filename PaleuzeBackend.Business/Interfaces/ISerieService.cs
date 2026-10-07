using PaleuzeBackend.Business.Models;

namespace PaleuzeBackend.Business.Interfaces
{
    public interface ISerieService
    {
        Task<IEnumerable<Serie>> GetAllByTournamentIdAsync(Guid tournamentId);
        Task<Serie> GetByIdAsync(Guid id);
    }
}
