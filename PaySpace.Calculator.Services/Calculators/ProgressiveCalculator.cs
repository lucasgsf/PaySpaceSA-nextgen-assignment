using PaySpace.Calculator.Borders.Models;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Builders;
using PaySpace.Calculator.Services.Abstractions.Calculators;
using PaySpace.Calculator.Services.Abstractions.Services;
using PaySpace.Calculator.Services.Abstractions.Strategies;

namespace PaySpace.Calculator.Services.Calculators;

internal sealed class ProgressiveCalculator(
    ICalculatorSettingsService _calculatorSettingsService,
    ICalculationServiceStrategy _calculationService,
    ICalculateResultBuilder _resultBuilder) : ITaxCalculator
{
    public CalculatorType CalculatorType => CalculatorType.Progressive;

    public async Task<CalculateResult> CalculateAsync(decimal income)
    {
        var lstSettings = await GetSuitableSettings(income);
        return await CalculateProgressiveSettingsTax(lstSettings, income);
    }

    private async Task<List<CalculatorSetting>> GetSuitableSettings(decimal income)
    {
        var lstSettings = await _calculatorSettingsService.GetSettingsAsync(CalculatorType);
        return lstSettings.Where(_ => _.From <= income).ToList();
    }

    private async Task<CalculateResult> CalculateSettingTax(CalculatorSetting setting, decimal settingIncome)
    {
        return await _calculationService.GetCalculationService(setting.RateType)
                                        .CalculateAsync(settingIncome, setting);
    }

    private async Task<CalculateResult> CalculateProgressiveSettingsTax(List<CalculatorSetting> lstSettings, decimal income)
    {
        decimal accumulatedTax = 0;
        decimal lastSettingLimit = 0;

        foreach (var setting in lstSettings)
        {
            var taxableAmount = Math.Min(income, setting.To ?? income) - lastSettingLimit;

            var settingTax = await CalculateSettingTax(setting, taxableAmount);
            accumulatedTax += settingTax.Tax;
            lastSettingLimit = setting.To ?? 0;
        }

        return _resultBuilder
                    .SetCalculatorType(CalculatorType)
                    .SetTax(accumulatedTax)
                    .Build();
    }
}