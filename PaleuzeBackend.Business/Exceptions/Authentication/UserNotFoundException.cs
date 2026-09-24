using PaleuzeBackend.Business.Models;

namespace PaleuzeBackend.Business.Exceptions.Authentication
{
    public class UserNotFoundException : PaleuzeException
    {
        public UserNotFoundException(string username)
            : base($"User \"{username}\" does not exist.") 
        { }

        public UserNotFoundException(Guid userId)
            : base($"User with ID \"{userId}\" does not exist.") 
        { }

        public UserNotFoundException(User user)
            : this(user.UserName)
        { }
    }
}
