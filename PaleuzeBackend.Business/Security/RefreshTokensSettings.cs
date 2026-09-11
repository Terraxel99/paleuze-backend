namespace PaleuzeBackend.Business.Security
{
    public class RefreshTokensSettings
    {
        public int RefreshTokenExpiryDays { get; set; }
        public int RefreshTokenMaximumCombinedSessionDays { get; set; }
    }
}
