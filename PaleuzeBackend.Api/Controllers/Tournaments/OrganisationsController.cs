using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PaleuzeBackend.Api.Authentication;
using PaleuzeBackend.Api.Models.Organisations;

using PaleuzeBackend.Business.Interfaces;
using PaleuzeBackend.Business.Models.Authentication;
using PaleuzeBackend.Business.Models.Organisations;

namespace PaleuzeBackend.Api.Controllers.Tournaments
{
    [Route("[controller]")]
    [ApiController]
    public class OrganisationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrganisationsService _organisationsService;

        public OrganisationsController(
            IMapper mapper,
            IOrganisationsService organisationsService) 
        {
            this._mapper = mapper;
            this._organisationsService = organisationsService;
        }

        [AllowAnonymous]
        [HttpGet("debug/claims")]
        public IActionResult GetClaims()
        {
            var claims = User.Claims.Select(c => new
            {
                c.Type,
                c.Value,
                c.Issuer
            });

            return Ok(claims);
        }

        [HttpGet]
        [AuthorizeRoles(UserRole.Admin)]
        public async Task<ActionResult<IEnumerable<OrganisationResponse>>> GetAll()
        {
            var organisations = await this._organisationsService.GetAllAsync();

            return this.Ok(this._mapper.Map<IEnumerable<OrganisationResponse>>(organisations));
        }

        [HttpGet("{id:guid}")]
        [AuthorizeRoles(UserRole.Admin, UserRole.TournamentViewer, UserRole.TournamentManager)]
        public async Task<ActionResult<OrganisationResponse>> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            var organisation = await this._organisationsService.GetByIdAsync(id);
            return this.Ok(this._mapper.Map<OrganisationResponse>(organisation));
        }

        [HttpPost]
        [AuthorizeRoles(UserRole.Admin, UserRole.TournamentManager)]
        public async Task<ActionResult<Guid>> Create([FromBody]OrganisationRequest organisation)
        {
            var id = await this._organisationsService.CreateAsync(
                this._mapper.Map<Organisation>(organisation));

            return this.Ok(id);
        }

        [HttpPut]
        [AuthorizeRoles(UserRole.Admin, UserRole.TournamentManager)]
        public async Task<ActionResult> Update(Guid id, [FromBody]OrganisationRequest organisation)
        {
            await this._organisationsService.UpdateAsync(
                id,
                this._mapper.Map<Organisation>(organisation));

            return this.NoContent();
        }

        [HttpPatch("{organisationId:guid}")]
        [AuthorizeRoles(UserRole.Admin, UserRole.TournamentManager)]
        public async Task<ActionResult> AddUser(Guid organisationId, [FromBody]Guid userId)
        {
            if (userId == Guid.Empty || organisationId == Guid.Empty)
            {
                return this.BadRequest();
            }

            await this._organisationsService.AddUserToOrganisationAsync(organisationId, userId);
            return this.NoContent();
        }

        [HttpDelete("{id:guid}")]
        [AuthorizeRoles(UserRole.Admin, UserRole.TournamentManager)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await this._organisationsService.DeleteAsync(id);
            return this.NoContent();
        }
    }
}
