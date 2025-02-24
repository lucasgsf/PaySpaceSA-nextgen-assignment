using PaySpace.Calculator.Borders.Models;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Abstractions.Services
{
    public interface ICalculationService
    {
        RateType RateType { get; }
        Task<CalculateResult> CalculateAsync(decimal income, CalculatorSetting settings);
    }
}