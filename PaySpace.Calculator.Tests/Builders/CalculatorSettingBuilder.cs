using Bogus;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Tests.Builders;

internal class CalculatorSettingBuilder
{
    private CalculatorSetting _data = new CalculatorSetting();

    public CalculatorSettingBuilder()
    {
        var faker = new Faker();

        _data = new CalculatorSetting
        {
            Calculator = faker.PickRandom<CalculatorType>(),
            RateType = faker.PickRandom<RateType>(),
            Rate = faker.Random.Decimal(),
            From = faker.Random.Decimal(),
            To = faker.Random.Decimal()
        };
    }

    public CalculatorSettingBuilder WithCalculator(CalculatorType calculator)
    {
        _data.Calculator = calculator;
        return this;
    }

    public CalculatorSettingBuilder WithRateType(RateType rateType)
    {
        _data.RateType = rateType;
        return this;
    }

    public CalculatorSettingBuilder WithRate(decimal rate)
    {
        _data.Rate = rate;
        return this;
    }

    public CalculatorSettingBuilder WithFrom(decimal from)
    {
        _data.From = from;
        return this;
    }

    public CalculatorSettingBuilder WithTo(decimal? to)
    {
        _data.To = to;
        return this;
    }

    public CalculatorSetting Build() => _data;
}