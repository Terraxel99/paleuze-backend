namespace PaleuzeBackend.Api.Models.Organisations
{
    public record OrganisationResponse
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ManagerId { get; set; }
    }
}
