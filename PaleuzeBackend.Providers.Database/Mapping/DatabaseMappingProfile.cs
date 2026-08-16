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
        }
    }
}