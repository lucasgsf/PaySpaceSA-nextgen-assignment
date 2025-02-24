using FluentValidation;
using PaySpace.Calculator.Borders.Constants;
using PaySpace.Calculator.Borders.Models;
using System.Net;

namespace PaySpace.Calculator.Borders.Validators;

public sealed class CalculateRequestValidator : AbstractValidator<CalculateRequest>
{
    public CalculateRequestValidator()
    {
        RuleFor(_ => _.PostalCode)
            .NotNull()
            .WithErrorCode(HttpStatusCode.BadRequest.ToString())
            .WithMessage(ErrorMessages.PostalCodeIsNull)
            .NotEmpty()
            .WithErrorCode(HttpStatusCode.BadRequest.ToString())
            .WithMessage(ErrorMessages.PostalCodeIsEmpty);

        RuleFor(_ => _.Income)
            .NotNull()
            .WithErrorCode(HttpStatusCode.BadRequest.ToString())
            .WithMessage(ErrorMessages.IncomeIsNull)
            .NotEmpty()
            .WithErrorCode(HttpStatusCode.BadRequest.ToString())
            .WithMessage(ErrorMessages.IncomeIsEmpty);
    }
}