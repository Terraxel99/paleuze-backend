using AutoMapper;

using PaleuzeBackend.Api.Models;
using PaleuzeBackend.Api.Models.Organisations;

using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Models.Organisations;

namespace PaleuzeBackend.Api.Mapping
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            this.CreateMap<Tournament, TournamentResponse>();
            this.CreateMap<TournamentRequest, Tournament>();

            this.CreateMap<Organisation, OrganisationResponse>()
                .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.ManagerUserId));
            this.CreateMap<OrganisationRequest, Organisation>();

            this.CreateMap<User, UserResponse>();
        }
    }
}