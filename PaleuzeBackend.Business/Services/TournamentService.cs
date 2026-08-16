using PaleuzeBackend.Business.Interfaces;
using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Repositories;

namespace PaleuzeBackend.Business.Services
{
    public class TournamentService : ITournamentService
    {
        private ITournamentRepository _tournamentRepository;

        public TournamentService(ITournamentRepository tournamentRepository)
        {
            this._tournamentRepository = tournamentRepository;
        }

        public async Task<IEnumerable<Tournament>> GetAll()
        {
            return await this._tournamentRepository.GetAll();
        }
    }
}