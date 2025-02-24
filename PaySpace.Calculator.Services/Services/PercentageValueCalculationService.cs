using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Borders.Models;
using PaySpace.Calculator.Services.Abstractions.Builders;
using PaySpace.Calculator.Services.Abstractions.Services;

namespace PaySpace.Calculator.Services.Services;

internal sealed class PercentageValueCalculationService(ICalculateResultBuilder _resultBuilder) : ICalculationService
{
    public RateType RateType => RateType.Percentage;

    public async Task<CalculateResult> CalculateAsync(decimal income, CalculatorSetting settings)
    {
        var tax = (income > 0) ? income * (settings.Rate / 100) : 0;

        return _resultBuilder
                    .SetCalculatorType(settings.Calculator)
                    .SetTax(tax)
                    .Build();
    }
}