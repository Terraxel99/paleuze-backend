using AutoMapper;

using Microsoft.EntityFrameworkCore;

using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Repositories;

namespace PaleuzeBackend.Providers.Database.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private TournamentDbContext _dbContext;
        private IMapper _mapper;

        public TournamentRepository(
            TournamentDbContext dbContext,
            IMapper mapper
        )
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<Tournament>> GetAll()
        {
            var tournaments = await this._dbContext.Tournaments.ToListAsync();

            return this._mapper.Map<IEnumerable<Tournament>>(tournaments);
        }
    }
}