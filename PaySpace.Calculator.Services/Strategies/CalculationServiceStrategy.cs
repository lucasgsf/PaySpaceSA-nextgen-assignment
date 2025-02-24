using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Services;
using PaySpace.Calculator.Services.Abstractions.Strategies;

namespace PaySpace.Calculator.Services.Strategies;

internal sealed class CalculationServiceStrategy : ICalculationServiceStrategy
{
    private readonly Dictionary<RateType, ICalculationService> _services;

    public CalculationServiceStrategy(IEnumerable<ICalculationService> services)
    {
        _services = services.ToDictionary(c => c.RateType, c => c);
    }

    public ICalculationService? GetCalculationService(RateType rateType)
    {
        return _services.TryGetValue(rateType, out var service) ? service : null;
    }
}