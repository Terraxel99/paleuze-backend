using AutoMapper;

using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Providers.Database.Entities;

namespace PaleuzeBackend.Providers.Database.Mapping
{
    public class DatabaseMappingProfile : Profile
    {
        public DatabaseMappingProfile()
        {
            this.CreateMap<TournamentEntity, Tournament>();

            // For business to entities, we do not want to map the ID's.
            // Because the ID's are PK, if we map the ID and insert in DB, EF Core will not let us insert. 
            // Ignoring ID's lets us map into an existing entity without touching the PK. 
            // (e.g : in case of an update, mapping from business would try to set another ID).
            this.CreateMap<Tournament, TournamentEntity>()
                .ForMember(model => model.Id, opt => opt.Ignore());
        }
    }
}