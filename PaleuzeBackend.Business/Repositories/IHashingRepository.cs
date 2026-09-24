namespace PaleuzeBackend.Business.Repositories
{
    public interface IHashingRepository
    {
        Task<string> SHA256HashAsync(string input);
    }
}
