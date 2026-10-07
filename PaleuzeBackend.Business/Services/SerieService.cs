using PaleuzeBackend.Business.Exceptions.Tournaments;
using PaleuzeBackend.Business.Interfaces;
using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Repositories;

namespace PaleuzeBackend.Business.Services
{
    public class SerieService : ISerieService
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ISerieRepository _serieRepository;

        public SerieService(
            ITournamentRepository tournamentRepository,
            ISerieRepository serieRepository
        )
        {
            this._tournamentRepository = tournamentRepository;
            this._serieRepository = serieRepository;
        }

        public async Task<IEnumerable<Serie>> GetAllByTournamentIdAsync(Guid tournamentId)
        {
            if (!await this._tournamentRepository.ExistsAsync(tournamentId))
            {
                throw new TournamentNotFoundException(tournamentId);
            }

            return await this._serieRepository.GetAllByTournamentIdAsync(tournamentId);
        }

        public async Task<Serie> GetByIdAsync(Guid id)
        {
            var serie = await this._serieRepository.GetByIdAsync(id);

            if (serie is null)
            {
                throw new SerieNotFoundException(id);
            }

            return serie;
        }
    }
}
