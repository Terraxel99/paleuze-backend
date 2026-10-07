using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using PaleuzeBackend.Api.Authentication;
using PaleuzeBackend.Api.Models;

using PaleuzeBackend.Business.Models;
using PaleuzeBackend.Business.Models.Authentication;
using PaleuzeBackend.Business.Interfaces;

namespace PaleuzeBackend.Api.Controllers.Tournaments
{
    [ApiController]
    [Route("[controller]")]
    public class TournamentsController : ControllerBase
    {
        private ITournamentService _tournamentService;
        private ISerieService _serieService;
        private IMapper _mapper;

        public TournamentsController(
            ITournamentService tournamentService,
            ISerieService serieService,
            IMapper mapper
        )
        {
            this._tournamentService = tournamentService;
            this._serieService = serieService;
            this._mapper = mapper;
        }

        [HttpGet]
        [AuthorizeRoles(UserRole.Admin, UserRole.TournamentManager, UserRole.TournamentViewer)]
        public async Task<ActionResult<IEnumerable<TournamentResponse>>> Get()
        {
            var tournaments = await this._tournamentService.GetAllAsync();

            return this.Ok(this._mapper.Map<IEnumerable<TournamentResponse>>(tournaments));
        }

        [HttpGet("{id:guid}")]
        [AuthorizeRoles(UserRole.Admin, UserRole.TournamentManager, UserRole.TournamentViewer)]
        public async Task<ActionResult<TournamentResponse>> Get(Guid id)
        {
            var tournament = await this._tournamentService.GetByIdAsync(id);

            return this.Ok(tournament);
        }

        [HttpGet("{id:guid}/series")]
        [AuthorizeRoles(UserRole.Admin, UserRole.TournamentManager, UserRole.TournamentViewer)]
        public async Task<ActionResult<IEnumerable<SerieResponse>>> GetSeries(Guid id)
        {
            var series = await this._serieService.GetAllByTournamentIdAsync(id);

            return this.Ok(this._mapper.Map<IEnumerable<SerieResponse>>(series));
        }

        [HttpPost]
        [AuthorizeRoles(UserRole.Admin, UserRole.TournamentManager)]
        public async Task<ActionResult<Guid>> Create(TournamentRequest tournament)
        {
            var guid = await this._tournamentService.CreateAsync(this._mapper.Map<Tournament>(tournament));

            return this.Ok(guid);
        }

        [HttpPut("{id:guid}")]
        [AuthorizeRoles(UserRole.Admin, UserRole.TournamentManager)]
        public async Task<ActionResult> Update(Guid id, [FromBody]TournamentRequest tournament)
        {
            await this._tournamentService.UpdateAsync(id, this._mapper.Map<Tournament>(tournament));

            return this.NoContent();
        }

        [HttpDelete]
        [AuthorizeRoles(UserRole.Admin, UserRole.TournamentManager)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await this._tournamentService.DeleteAsync(id);

            return this.NoContent();
        }
    }
}