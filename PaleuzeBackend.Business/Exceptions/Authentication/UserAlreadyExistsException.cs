namespace PaleuzeBackend.Business.Exceptions.Authentication
{
    public class UserAlreadyExistsException : PaleuzeException
    {
        public UserAlreadyExistsException(string username)
            : base($"User \"{username}\" already exists.")
        { }
    }
}
