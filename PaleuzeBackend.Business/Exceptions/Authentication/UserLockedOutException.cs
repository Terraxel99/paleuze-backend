namespace PaleuzeBackend.Business.Exceptions.Authentication
{
    public class UserLockedOutException : PaleuzeException
    {
        public string UserName { get; set; }

        public UserLockedOutException(string username)
            : base($"User \"\" is locked out.")
        {
            this.UserName = username;
        }
    }
}
