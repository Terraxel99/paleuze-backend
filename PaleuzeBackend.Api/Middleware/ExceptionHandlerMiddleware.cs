using Microsoft.AspNetCore.Diagnostics;

using PaleuzeBackend.Business.Exceptions.Authentication;

namespace PaleuzeBackend.Api.Middleware
{
    public class ExceptionHandlerMiddleware : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var status = exception switch
            {
                InvalidCredentialsException => StatusCodes.Status401Unauthorized,
                UserLockedOutException => StatusCodes.Status403Forbidden,
                UserNotFoundException => StatusCodes.Status404NotFound,
                UserAlreadyExistsException => StatusCodes.Status409Conflict,

                _ => 0,
            };

            if (status == 0)
            {
                return false;
            }

            httpContext.Response.StatusCode = status;
            return true;
        }
    }
}
