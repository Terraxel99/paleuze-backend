namespace PaleuzeBackend.Business.Models.Authentication
{
    public record UserToken
    {
        public required User User { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required DateTime RefreshTokenExpiry { get; set; }
    }
}
