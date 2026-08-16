using PaleuzeBackend.Business.Models;

namespace PaleuzeBackend.Business.Interfaces
{
    public interface ITournamentService
    {
        Task<IEnumerable<Tournament>> GetAll();
    }
}