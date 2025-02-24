using Bogus;
using FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using PaySpace.Calculator.Borders.Constants;
using PaySpace.Calculator.Borders.Models;
using PaySpace.Calculator.Borders.Validators;
using PaySpace.Calculator.Tests.Builders;
using System.Collections;
using System.Net;

namespace PaySpace.Calculator.Tests.Validators;

[TestFixture]
internal class CalculateRequestValidatorTest
{
    private CalculateRequestValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new CalculateRequestValidator();
    }

    [TestCaseSource(nameof(InvalidRequestCases))]
    public void Verify_Validators_Exceptions(CalculateRequest request, string errorMessage)
    {
        // Act
        var result = _validator.TestValidate(request);
        
        // Assert
        result
            .ShouldHaveValidationErrorFor(_ => _.PostalCode)
            .WithErrorMessage(errorMessage)
            .WithErrorCode(HttpStatusCode.BadRequest.ToString());
    }

    public static IEnumerable InvalidRequestCases()
    {
        yield return new TestCaseData(
            new CalculateRequestBuilder().WithPostalCode("").Build(),
            ErrorMessages.PostalCodeIsEmpty
        );
        yield return new TestCaseData(
            new CalculateRequestBuilder().WithPostalCode(null).Build(),
            ErrorMessages.PostalCodeIsNull
        );
    }
}