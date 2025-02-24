using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Borders.Models;
using PaySpace.Calculator.Services.Abstractions.Builders;
using PaySpace.Calculator.Services.Abstractions.Services;

namespace PaySpace.Calculator.Services.Services;

internal sealed class AmountValueCalculationService(ICalculateResultBuilder _resultBuilder) : ICalculationService
{
    public RateType RateType => RateType.Amount;

    public async Task<CalculateResult> CalculateAsync(decimal income, CalculatorSetting settings)
    {
        return _resultBuilder
                    .SetCalculatorType(settings.Calculator)
                    .SetTax(settings.Rate)
                    .Build();
    }
}