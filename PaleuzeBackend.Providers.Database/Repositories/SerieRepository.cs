using AutoMapper;

using Microsoft.EntityFrameworkCore;
using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Repositories;

using PaleuzeBackend.Providers.Database.Data;

namespace PaleuzeBackend.Providers.Database.Repositories
{
    public class SerieRepository : ISerieRepository
    {
        private readonly TournamentDbContext _database;
        private readonly IMapper _mapper;

        public SerieRepository(
            TournamentDbContext dbContext,
            IMapper mapper
        ) 
        {
            this._database = dbContext;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<Serie>> GetAllByTournamentIdAsync(Guid tournamentId)
        {
            var series = await this._database.Series
                .AsNoTracking()
                .Where(s => s.TournamentId == tournamentId)
                .ToListAsync();

            return this._mapper.Map<IEnumerable<Serie>>(series);
        }

        public async Task<Serie> GetByIdAsync(Guid id)
        {
            var serie = await this._database.Series
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.Id == id);

            return this._mapper.Map<Serie>(serie);
        }
    }
}
