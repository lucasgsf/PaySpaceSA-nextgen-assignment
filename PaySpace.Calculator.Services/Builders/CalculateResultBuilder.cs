using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Borders.Models;
using PaySpace.Calculator.Services.Abstractions.Builders;

namespace PaySpace.Calculator.Services.Builders;

internal sealed class CalculateResultBuilder() : ICalculateResultBuilder
{
    private CalculateResult _result = new();

    public CalculateResult Build() => _result;

    public ICalculateResultBuilder SetCalculatorType(CalculatorType type)
    {
        _result = _result with { Calculator = type };
        return this;
    }

    public ICalculateResultBuilder SetTax(decimal tax)
    {
        _result = _result with { Tax = tax };
        return this;
    }
}