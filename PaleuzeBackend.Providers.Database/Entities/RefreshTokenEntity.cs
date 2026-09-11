namespace PaleuzeBackend.Providers.Database.Entities
{
    public record RefreshTokenEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string TokenHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime AbsoluteExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        public string? ReplacedByTokenHash { get; set; }
        public UserEntity User { get; set; } = null!;
    }
}
