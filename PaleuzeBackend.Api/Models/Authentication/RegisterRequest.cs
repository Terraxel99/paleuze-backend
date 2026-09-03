namespace PaleuzeBackend.Api.Models
{
    public record RegisterRequest
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
