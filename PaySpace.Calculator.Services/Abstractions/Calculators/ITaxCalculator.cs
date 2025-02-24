using PaySpace.Calculator.Borders.Models;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Abstractions.Calculators;

public interface ITaxCalculator
{
    CalculatorType CalculatorType { get; }
    Task<CalculateResult> CalculateAsync(decimal income);
}