using PaleuzeBackend.Business.Models;

namespace PaleuzeBackend.Business.Exceptions.Authentication
{
    public class UserLockedOutException : PaleuzeException
    {
        public UserLockedOutException(User user)
            : base($"User \"{user.UserName}\" is locked out.") 
        { }
    }
}
