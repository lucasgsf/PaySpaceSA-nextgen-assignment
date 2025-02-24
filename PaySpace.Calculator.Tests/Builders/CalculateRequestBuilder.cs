using Bogus;
using PaySpace.Calculator.Borders.Models;

namespace PaySpace.Calculator.Tests.Builders;

internal class CalculateRequestBuilder
{
    private CalculateRequest _data = new CalculateRequest();

    public CalculateRequestBuilder()
    {
        var faker = new Faker();

        _data = new CalculateRequest
        {
            Income = faker.Random.Decimal(),
            PostalCode = faker.Random.String()
        };
    }

    public CalculateRequestBuilder WithIncome(decimal income)
    {
        _data.Income = income;
        return this;
    }

    public CalculateRequestBuilder WithPostalCode(string postalCode)
    {
        _data.PostalCode = postalCode;
        return this;
    }

    public CalculateRequest Build() => _data;
}