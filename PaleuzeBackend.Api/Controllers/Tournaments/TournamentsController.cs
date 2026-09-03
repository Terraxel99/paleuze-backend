using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaleuzeBackend.Api.Models;

using PaleuzeBackend.Business.Interfaces;
using PaleuzeBackend.Business.Models;

namespace PaleuzeBackend.Api.Controllers.Tournaments
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<TournamentResponse>>> Get()
        {
            var tournaments = await this._tournamentService.GetAllAsync();

            return this.Ok(this._mapper.Map<IEnumerable<TournamentResponse>>(tournaments));
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<TournamentResponse>> Get(Guid id)
        {
            var tournament = await this._tournamentService.GetByIdAsync(id);

            return this.Ok(tournament);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(TournamentRequest tournament)
        {
            var guid = await this._tournamentService.CreateAsync(this._mapper.Map<Tournament>(tournament));

            return this.Ok(guid);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, [FromBody]TournamentRequest tournament)
        {
            await this._tournamentService.UpdateAsync(id, this._mapper.Map<Tournament>(tournament));

            return this.NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            await this._tournamentService.DeleteAsync(id);

            return this.NoContent();
        }
    }
}