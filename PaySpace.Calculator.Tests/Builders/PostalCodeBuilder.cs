using Bogus;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Tests.Builders;

internal class PostalCodeBuilder
{
    private PostalCode _data = new PostalCode();

    public PostalCodeBuilder()
    {
        var faker = new Faker();

        _data = new PostalCode
        {
            Calculator = faker.PickRandom<CalculatorType>(),
            Code = faker.Random.String2(5),
            Id = faker.Random.Long()
        };
    }

    public PostalCodeBuilder WithCalculator(CalculatorType calculator)
    {
        _data.Calculator = calculator;
        return this;
    }

    public PostalCodeBuilder WithCode(string code)
    {
        _data.Code = code;
        return this;
    }

    public PostalCodeBuilder WithId(long id)
    {
        _data.Id = id;
        return this;
    }

    public PostalCode Build() => _data;
}