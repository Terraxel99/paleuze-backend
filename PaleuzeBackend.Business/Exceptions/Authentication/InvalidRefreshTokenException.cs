using PaleuzeBackend.Business.Models;

namespace PaleuzeBackend.Business.Exceptions.Authentication
{
    public class InvalidRefreshTokenException : PaleuzeException
    {
        public InvalidRefreshTokenException(string refreshToken)
            : base($"Invalid refresh token: {refreshToken}") 
        { }
    }
}
