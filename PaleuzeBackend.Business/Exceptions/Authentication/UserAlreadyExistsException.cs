namespace PaleuzeBackend.Business.Exceptions.Authentication
{
    public class UserAlreadyExistsException : PaleuzeException
    {
        public string UserName { get; set; }

        public UserAlreadyExistsException(string username)
            : base($"User \"{username}\" already exists.")
        {
            this.UserName = username;
        }
    }
}
