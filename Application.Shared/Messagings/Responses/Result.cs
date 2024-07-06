using System.Net;

namespace Application.Shared.Messagings.Responses;

public class Result<T>
{
    public T Value { get; set; }
    public bool IsSuccess { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public Error Error { get; set; }
    private Result(T value, HttpStatusCode httpStatusCode = default)
    {
        Value = value;
        StatusCode = httpStatusCode;
        IsSuccess = true;
    }
    private Result(Error error, HttpStatusCode httpStatusCode = default)
    {
        Error = error;
        IsSuccess = false;
    }

   public static Result<T> OnSucess(T value, HttpStatusCode httpStatusCode = default) => new(value, httpStatusCode);
   public static Result<T> OnFailure(Error error, HttpStatusCode httpStatusCode = default) => new(error, httpStatusCode);
}

public enum Error
{
    Failed,
    BadRequest,
    InvalidRequest,
    InvalidId
}
