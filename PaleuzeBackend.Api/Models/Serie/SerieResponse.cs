namespace PaleuzeBackend.Api
{
    public record SerieResponse
    {
        public required Guid Id { get; set; }
        public required Guid TournamentId { get; set; }
        public required string Name { get; set; }
        public bool IsDoubles { get; set; }
    }
}
