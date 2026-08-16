namespace PaleuzeBackend.Api.Models
{
    public record TournamentResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}