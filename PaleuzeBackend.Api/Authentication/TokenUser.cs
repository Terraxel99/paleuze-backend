namespace PaleuzeBackend.Api.Authentication
{
    public record TokenUser
    {
        public required string UserName { get; set; }
        public required IEnumerable<string> Roles { get; set; }
    }
}
