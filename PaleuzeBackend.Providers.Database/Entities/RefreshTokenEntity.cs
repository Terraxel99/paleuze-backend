namespace PaleuzeBackend.Providers.Database.Entities
{
    public record RefreshTokenEntity
    {
        public required Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required string TokenHash { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime ExpiresAt { get; set; }
        public required DateTime AbsoluteExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        public Guid? ReplacedByTokenId { get; set; }
        public UserEntity User { get; set; } = null!;
    }
}
