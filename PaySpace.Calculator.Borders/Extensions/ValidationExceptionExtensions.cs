using FluentValidation;
using PaySpace.Calculator.Borders.Shared;

namespace PaySpace.Calculator.Borders.Extensions;

public static class ValidationExceptionExtensions
{
    public static IEnumerable<ErrorMessage> ToErrorMessages(this ValidationException ex) =>
        ex.Errors.Select(error => new ErrorMessage(error.ErrorMessage)).DefaultIfEmpty(new ErrorMessage(ex.Message));
}