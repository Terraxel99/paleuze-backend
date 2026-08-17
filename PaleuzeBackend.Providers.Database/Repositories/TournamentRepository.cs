using AutoMapper;

using Microsoft.EntityFrameworkCore;

using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Repositories;

using PaleuzeBackend.Providers.Database.Entities;

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
            var tournaments = await this._dbContext.Tournaments
                .AsNoTracking()
                .ToListAsync();

            return this._mapper.Map<IEnumerable<Tournament>>(tournaments);
        }

        public async Task<Tournament> GetById(Guid id)
        {
            var entity = await this._dbContext.Tournaments
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
            
            if (entity is null)
            {
                throw new KeyNotFoundException();
            }

            return this._mapper.Map<Tournament>(entity);
        }

        public async Task<Guid> Create(Tournament tournament)
        {
            var entity = this._mapper.Map<TournamentEntity>(tournament);

            await this._dbContext.Tournaments.AddAsync(entity);
            await this._dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Update(Guid id, Tournament tournament)
        {
            var entity = await this._dbContext.Tournaments
                .FirstOrDefaultAsync(t => t.Id == id);
                
            if (entity is null)
            {
                throw new KeyNotFoundException();
            }

            this._mapper.Map(tournament, entity);
            await this._dbContext.SaveChangesAsync();          
        }

        public async Task Delete(Guid id)
        {
            var entity = await this._dbContext.Tournaments
                .FindAsync(id);

            if (entity is null)
            {
                throw new KeyNotFoundException();
            }

            this._dbContext.Tournaments.Remove(entity);
            await this._dbContext.SaveChangesAsync();
        }
    }
}