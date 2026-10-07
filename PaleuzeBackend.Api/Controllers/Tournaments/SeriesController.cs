using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using PaleuzeBackend.Api.Authentication;
using PaleuzeBackend.Api.Models;
using PaleuzeBackend.Business.Interfaces;
using PaleuzeBackend.Business.Models.Authentication;

namespace PaleuzeBackend.Api.Controllers.Tournaments
{
    [Route("[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISerieService _serieService;
        private readonly IMapper _mapper;

        public SeriesController(
          ISerieService serieService,
          IMapper mapper
        )
        {
            this._serieService = serieService;
            this._mapper = mapper;
        }

        [HttpGet("{id:guid}")]
        [AuthorizeRoles(UserRole.Admin, UserRole.TournamentManager, UserRole.TournamentViewer)]
        public async Task<ActionResult<SerieResponse>> Get(Guid id)
        {
            var serie = this._mapper.Map<SerieResponse>(await this._serieService.GetByIdAsync(id));

            return this.Ok(serie);
        }

        [HttpPost]
        [AuthorizeRoles(UserRole.Admin, UserRole.TournamentManager)]
        public async Task<ActionResult<Guid>> Create([FromBody]SerieRequest serie)
        {
            return this.NotFound();
        }

        [HttpPut]
        [AuthorizeRoles(UserRole.Admin, UserRole.TournamentManager)]
        public async Task<ActionResult> Update(Guid id, [FromBody]SerieRequest serie)
        {
            return this.NotFound();
        }

        [HttpDelete]
        [AuthorizeRoles(UserRole.Admin, UserRole.TournamentManager)]
        public async Task<ActionResult> Delete(Guid id)
        {
            return this.NotFound();
        }
    }
}
