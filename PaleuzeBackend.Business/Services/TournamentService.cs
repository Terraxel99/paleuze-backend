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

        public async Task<Tournament> GetById(Guid id)
        {
            return await this._tournamentRepository.GetById(id);
        }

        public async Task<Guid> Create(Tournament tournament)
        {
            return await this._tournamentRepository.Create(tournament);
        }

        public async Task Update(Guid id, Tournament tournament)
        {
            await this._tournamentRepository.Update(id, tournament);
        }

        public async Task Delete(Guid id)
        {
            await this._tournamentRepository.Delete(id);
        }
    }
}