using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using PaleuzeBackend.Api.Models;
using PaleuzeBackend.Business.Interfaces;

namespace PaleuzeBackend.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TournamentsController : ControllerBase
    {
        private ITournamentService _tournamentService;
        private IMapper _mapper;

        public TournamentsController(
            ITournamentService tournamentService,
            IMapper mapper
        )
        {
            this._tournamentService = tournamentService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentResponse>>> Get()
        {
            var result = await this._tournamentService.GetAll();

            return this.Ok(this._mapper.Map<IEnumerable<TournamentResponse>>(result));
        }
    }
}