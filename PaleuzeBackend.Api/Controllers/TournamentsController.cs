using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using PaleuzeBackend.Api.Models;

using PaleuzeBackend.Business.Interfaces;
using PaleuzeBackend.Business.Models;

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
            var tournaments = await this._tournamentService.GetAll();

            return this.Ok(this._mapper.Map<IEnumerable<TournamentResponse>>(tournaments));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TournamentResponse>> Get(Guid id)
        {
            var tournament = await this._tournamentService.GetById(id);

            return this.Ok(tournament);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(TournamentRequest tournament)
        {
            var guid = await this._tournamentService.Create(this._mapper.Map<Tournament>(tournament));

            return this.Ok(guid);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, [FromBody]TournamentRequest tournament)
        {
            await this._tournamentService.Update(id, this._mapper.Map<Tournament>(tournament));

            return this.NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            await this._tournamentService.Delete(id);

            return this.NoContent();
        }
    }
}