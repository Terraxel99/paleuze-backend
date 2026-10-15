namespace PaleuzeBackend.Business.Interfaces
{
    public interface ICurrentUser
    {
        Guid? Id { get; }
        bool IsAuthenticated { get; }
        bool IsAdmin { get; }
        bool IsInRole(string role);
    }
}
