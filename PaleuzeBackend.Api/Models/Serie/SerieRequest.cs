namespace PaleuzeBackend.Api.Models
{
    public record SerieRequest
    {
        public required string Name { get; set; }
        public required Guid TournamentId { get; set; }
        public required bool IsDoubles { get; set; }
    }
}
