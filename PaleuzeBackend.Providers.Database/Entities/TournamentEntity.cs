namespace PaleuzeBackend.Providers.Database.Entities
{
    public record TournamentEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}