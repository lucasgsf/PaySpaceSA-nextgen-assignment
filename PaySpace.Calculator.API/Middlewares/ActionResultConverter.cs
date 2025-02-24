using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.API.Abstractions;
using PaySpace.Calculator.Borders.Shared;
using System.Net;

namespace PaySpace.Calculator.API.Middlewares;

public class ActionResultConverter : IActionResultConverter
{
    private readonly string path;

    public ActionResultConverter(IHttpContextAccessor accessor)
    {
        path = accessor.HttpContext!.Request.Path.Value!;
    }

    public IActionResult Convert<Tin>(UseCaseResponse<Tin> response) where Tin : class
    {
        return Convert<Tin, Tin>(response);
    }

    public IActionResult Convert<Tin, Tout>(UseCaseResponse<Tin> response, Func<Tin?, Tout?>? converter = null) where Tin : class where Tout : class
    {
        if (response == null)
            return BuildError(new[] { new ErrorMessage("ActionResultConverter Error") }, HttpStatusCode.InternalServerError);

        if (response.Success())
        {
            if (converter is null)
                return BuildSuccessResult(response.Result, response.ResultId, response.Status);

            return BuildSuccessResult(converter.Invoke(response.Result), response.ResultId, response.Status);
        }

        var hasErrors = !response.Errors!.Any();
        var errorResult = hasErrors ? new[] { new ErrorMessage(response.ErrorMessage ?? "Unknown error") } : response.Errors;

        return BuildError(errorResult!, response.Status);
    }

    private IActionResult BuildSuccessResult(object? data, string id, HttpStatusCode status)
    {
        return status switch
        {
            HttpStatusCode.Created => new CreatedResult($"{path}/{id}", data),
            HttpStatusCode.NoContent => new NoContentResult(),
            HttpStatusCode.Accepted => new AcceptedResult($"{path}/{id}", data),
            _ => new OkObjectResult(data),
        };
    }

    private ObjectResult BuildError(object data, HttpStatusCode status)
    {
        return new ObjectResult(data)
        {
            StatusCode = (int)status
        };
    }
}