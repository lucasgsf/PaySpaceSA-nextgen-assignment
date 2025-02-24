using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Calculators;

namespace PaySpace.Calculator.Services.Abstractions.Strategies;

public interface ITaxCalculatorStrategy
{
    ITaxCalculator GetCalculator(CalculatorType calculatorType);
}