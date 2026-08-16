namespace PaleuzeBackend.Business.Models
{
    public record Tournament
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
    }
}