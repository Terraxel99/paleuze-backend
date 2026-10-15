namespace PaleuzeBackend.Business.Exceptions.General
{
    public class InsufficientPermissionsException : PaleuzeException
    {
        public InsufficientPermissionsException(Guid? userId, string action) 
            : base($"User with ID \"{userId}\" has insufficient privileges. Requested -> {action}")
        {
        }
    }
}
