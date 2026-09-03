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

        public async Task<IEnumerable<Tournament>> GetAllAsync()
        {
            return await this._tournamentRepository.GetAllAsync();
        }

        public async Task<Tournament> GetByIdAsync(Guid id)
        {
            return await this._tournamentRepository.GetByIdAsync(id);
        }

        public async Task<Guid> CreateAsync(Tournament tournament)
        {
            return await this._tournamentRepository.CreateAsync(tournament);
        }

        public async Task UpdateAsync(Guid id, Tournament tournament)
        {
            await this._tournamentRepository.UpdateAsync(id, tournament);
        }

        public async Task DeleteAsync(Guid id)
        {
            await this._tournamentRepository.DeleteAsync(id);
        }
    }
}