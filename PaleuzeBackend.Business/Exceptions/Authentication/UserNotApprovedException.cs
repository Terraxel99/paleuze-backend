using PaleuzeBackend.Business.Models;

namespace PaleuzeBackend.Business.Exceptions.Authentication
{
    public class UserNotApprovedException : PaleuzeException
    {
        public UserNotApprovedException(User user) 
            : base($"User \"{user.UserName}\" is not approved by administrator.")
        { }
    }
}
