namespace PaleuzeBackend.Providers.Database.Entities
{
    public record SerieEntity
    {
        public required Guid Id { get; set; }
        public required Guid TournamentId { get; set; }
        public required string Name { get; set; }
        public bool IsDoubles { get; set; }

        public TournamentEntity Tournament { get; set; } = null!;
    }
}
