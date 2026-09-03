namespace PaleuzeBackend.Api.Models
{
    public record LoginResponse
    {
        public required string Token { get; set; }
        public required UserResponse User { get; set; }
    }
}
