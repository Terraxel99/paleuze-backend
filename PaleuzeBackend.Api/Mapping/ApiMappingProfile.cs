using AutoMapper;

using PaleuzeBackend.Api.Models;
using PaleuzeBackend.Business.Models;

namespace PaleuzeBackend.Api.Mapping
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            this.CreateMap<Tournament, TournamentResponse>();
            this.CreateMap<TournamentRequest, Tournament>();
        }
    }
}