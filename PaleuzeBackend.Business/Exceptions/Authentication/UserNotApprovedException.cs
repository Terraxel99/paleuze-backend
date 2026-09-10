namespace PaleuzeBackend.Business.Exceptions.Authentication
{
    internal class UserNotApprovedException : PaleuzeException
    {
        public UserNotApprovedException(string username) 
            : base($"User \"{username}\" is not approved by administrator.")
        { }
    }
}
