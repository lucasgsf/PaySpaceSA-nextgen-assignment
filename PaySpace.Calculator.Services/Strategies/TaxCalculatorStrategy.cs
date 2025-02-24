using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Calculators;
using PaySpace.Calculator.Services.Abstractions.Strategies;

namespace PaySpace.Calculator.Services.Strategies;

internal sealed class TaxCalculatorStrategy : ITaxCalculatorStrategy
{
    private readonly Dictionary<CalculatorType, ITaxCalculator> _calculators;

    public TaxCalculatorStrategy(IEnumerable<ITaxCalculator> calculators)
    {
        _calculators = calculators.ToDictionary(c => c.CalculatorType, c => c);
    }

    public ITaxCalculator? GetCalculator(CalculatorType calculatorType)
    {
        return _calculators.TryGetValue(calculatorType, out var calculator) ? calculator : null;
    }
}