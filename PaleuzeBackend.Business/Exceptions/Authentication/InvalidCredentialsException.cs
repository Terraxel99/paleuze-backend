namespace PaleuzeBackend.Business.Exceptions.Authentication
{
    public class InvalidCredentialsException : PaleuzeException
    {
        public InvalidCredentialsException()
            : base("Credentials are invalid.") { }
    }
}
