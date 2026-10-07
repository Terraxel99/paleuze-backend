using AutoMapper;

using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Models.Authentication;
using PaleuzeBackend.Providers.Database.Entities;

namespace PaleuzeBackend.Providers.Database.Mapping
{
    public class DatabaseMappingProfile : Profile
    {
        public DatabaseMappingProfile()
        {
            this.CreateMap<TournamentEntity, Tournament>();
            this.CreateMap<Tournament, TournamentEntity>()
                .ForMember(model => model.Id, opt => opt.Ignore());

            this.CreateMap<UserEntity, User>();

            this.CreateMap<RefreshToken, RefreshTokenEntity>()
                .ForMember(model => model.Id, opt => opt.Ignore());
        }
    }
}