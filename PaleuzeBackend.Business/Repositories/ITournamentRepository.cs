using PaleuzeBackend.Business.Models;

namespace PaleuzeBackend.Business.Repositories
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<Tournament>> GetAll();
    }
}