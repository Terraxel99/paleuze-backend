namespace PaleuzeBackend.Providers.Database.Entities
{
    public record OrganisationEntity
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required Guid ManagerUserId { get; set; }
    }
}
