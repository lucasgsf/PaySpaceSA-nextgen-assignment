using Bogus;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Tests.Builders;

internal class CalculatorHistoryBuilder
{
    private CalculatorHistory _data = new CalculatorHistory();

    public CalculatorHistoryBuilder()
    {
        var faker = new Faker();

        _data = new CalculatorHistory
        {
            Calculator = faker.PickRandom<CalculatorType>(),
            Income = faker.Random.Decimal(),
            PostalCode = faker.Random.String(),
            Tax = faker.Random.Decimal()
        };
    }

    public CalculatorHistory Build() => _data;
}