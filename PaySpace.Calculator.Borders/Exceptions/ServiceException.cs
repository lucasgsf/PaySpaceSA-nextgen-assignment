using PaySpace.Calculator.Borders.Shared;
using System.Net;

namespace PaySpace.Calculator.Borders.Exceptions;

public sealed class ServiceException : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    public IEnumerable<ErrorMessage> Errors { get; set; }
    
    public ServiceException(
        string message,
        HttpStatusCode statusCode) : base(message)
    {
        Errors = [ new ErrorMessage(message) ];
        StatusCode = statusCode;
    }
}