using PaySpace.Calculator.Borders.Models;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Abstractions.Builders;

public interface ICalculateResultBuilder
{
    ICalculateResultBuilder SetCalculatorType(CalculatorType type);
    ICalculateResultBuilder SetTax(decimal tax);
    CalculateResult Build();
}