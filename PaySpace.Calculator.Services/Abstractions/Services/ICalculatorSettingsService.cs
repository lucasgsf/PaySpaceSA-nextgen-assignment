using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Abstractions.Services
{
    public interface ICalculatorSettingsService
    {
        Task<List<CalculatorSetting>> GetSettingsAsync(CalculatorType calculatorType);
        Task<CalculatorSetting> GetSettingAsync(CalculatorType calculatorType, decimal income);
    }
}