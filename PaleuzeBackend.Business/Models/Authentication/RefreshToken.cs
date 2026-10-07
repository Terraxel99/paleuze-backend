namespace PaleuzeBackend.Business.Models.Authentication
{
    public record RefreshToken
    {
        public Guid UserId { get; set; }
        public required string Token { get; set; }
        public required string TokenHash { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime AbsoluteExpiresAt { get; set; }
    }
}