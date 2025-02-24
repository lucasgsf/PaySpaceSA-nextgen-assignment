using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Services;

namespace PaySpace.Calculator.Services.Abstractions.Strategies;

public interface ICalculationServiceStrategy
{
    ICalculationService GetCalculationService(RateType rateType);
}