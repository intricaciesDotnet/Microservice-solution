using Application.Shared.Messagings.Responses;
using System.Net;

namespace user.api.Exceptions;

public sealed class UserException(string message) : Exception(message)
{
}

public static class UserExceptionHelper
{
    public static Result<T> Execute<T>(this UserException exception, Error error)
    {
        return Result<T>.OnFailure(error, GetCode(error));

        static HttpStatusCode GetCode(Error error) => error switch
        {
            Error.InvalidRequest => HttpStatusCode.BadRequest,
            Error.BadRequest => HttpStatusCode.BadRequest,
            Error.InvalidId => HttpStatusCode.BadRequest,
            _=> HttpStatusCode.BadRequest,
        };
    }
}
