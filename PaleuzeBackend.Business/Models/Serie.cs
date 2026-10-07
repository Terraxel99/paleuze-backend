namespace PaleuzeBackend.Business.Models
{
    public record Serie
    {
        public required Guid Id { get; set; }
        public required Guid TournamentId { get; set; }
        public required string Name { get; set; }
        public bool IsDoubles { get; set; }
    }
}
