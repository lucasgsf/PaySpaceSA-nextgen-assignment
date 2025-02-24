using FluentValidation;
using PaySpace.Calculator.Borders.Constants;
using PaySpace.Calculator.Borders.Exceptions;
using PaySpace.Calculator.Borders.Shared;
using PaySpace.Calculator.Borders.Extensions;
using System.Net;
using System.Text.Json;

namespace PaySpace.Calculator.API.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, logger);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception,
        ILogger<ErrorHandlingMiddleware> logger)
    {
        var (code, errorMessage, errors) = exception switch
        {
            ValidationException ex => (HttpStatusCode.BadRequest, ex.Message, ex.ToErrorMessages()),
            ServiceException ex => (ex.StatusCode, ex.Message, ex.Errors),
            _ => (HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError, new ErrorMessage[] { new(ErrorMessages.InternalServerError) })
        };

        if (errorMessage is not null)
            logger.LogError(exception, "{@ErrorMessage} - {@Errors}", errorMessage, errors);

        return BuildResponse(context, code, errors);
    }

    private static Task BuildResponse(HttpContext context, HttpStatusCode code, IEnumerable<ErrorMessage> errors)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(JsonSerializer.Serialize(errors,
            new JsonSerializerOptions(JsonSerializerDefaults.Web)));
    }
}