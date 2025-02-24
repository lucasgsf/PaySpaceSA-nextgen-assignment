using System.Net;

namespace PaySpace.Calculator.Borders.Shared;

public class UseCaseResponse<T>
{
    public HttpStatusCode Status { get; private set; }
    public T? Result { get; private set; }
    public string? ResultId { get; private set; }
    public string? ErrorMessage { get; private set; }
    public IEnumerable<ErrorMessage>? Errors { get; private set; } = Array.Empty<ErrorMessage>();
    public Dictionary<string, string>? Headers { get; private set; }

    private UseCaseResponse(HttpStatusCode status, T result, string? resultId = null, Dictionary<string, string>? headers = null)
    {
        Status = status;
        Result = result;
        ResultId = resultId;
        Headers = headers;
    }

    protected UseCaseResponse(HttpStatusCode status, string? errorMessage = null, IEnumerable<ErrorMessage>? errors = null)
    {
        ErrorMessage = errorMessage;
        Status = status;
        Errors = errors;
    }

    public static UseCaseResponse<T> Success(T result, Dictionary<string, string>? headers) => new(HttpStatusCode.OK, result, headers: headers);
    public static UseCaseResponse<T> Success(T result, KeyValuePair<string, string>? header = null) => new(HttpStatusCode.OK, result, headers: header != null ? new Dictionary<string, string> { { header.Value.Key, header.Value.Value } } : null);
    public static UseCaseResponse<T> Accepted(T result, KeyValuePair<string, string>? header = null) => new(HttpStatusCode.Accepted, result, headers: header != null ? new Dictionary<string, string> { { header.Value.Key, header.Value.Value } } : null);
    public static UseCaseResponse<T> BadRequest(string message) => BadRequest(new ErrorMessage[] { new(message) });

    public static UseCaseResponse<T> BadRequest(IEnumerable<ErrorMessage> errors) => new(HttpStatusCode.BadRequest, "Bad Request", errors);

    public static UseCaseResponse<T> NotFound(string errorMessage) => new(HttpStatusCode.NotFound, "Data not found",
        new List<ErrorMessage> { new(errorMessage) });

    public static UseCaseResponse<T> InternalServerError(string errorMessage) => new(HttpStatusCode.InternalServerError, "Internal Server Error",
        new List<ErrorMessage> { new(errorMessage) });

    public bool Success() => string.IsNullOrEmpty(ErrorMessage) && !(Errors?.Any() ?? false);
}