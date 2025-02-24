using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Borders.Models;
using PaySpace.Calculator.Services.Abstractions.Strategies;
using PaySpace.Calculator.Services.Abstractions.Calculators;
using PaySpace.Calculator.Services.Abstractions.Services;

namespace PaySpace.Calculator.Services.Calculators;

internal sealed class FlatRateCalculator(
    ICalculatorSettingsService _calculatorSettingsService,
    ICalculationServiceStrategy _calculationService) : ITaxCalculator
{
    public CalculatorType CalculatorType => CalculatorType.FlatRate;

    public async Task<CalculateResult> CalculateAsync(decimal income)
    {
        var setting = await _calculatorSettingsService.GetSettingAsync(CalculatorType, income);

        return await _calculationService.GetCalculationService(setting.RateType)
                                        .CalculateAsync(income, setting);
    }
}