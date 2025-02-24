using PaySpace.Calculator.Web.Borders.Models;
using PaySpace.Calculator.Web.Data.Abstractions;
using PaySpace.Calculator.Web.Services.Abstractions;

namespace PaySpace.Calculator.Web.Services.Services;

public class CalculatorService(ICalculatorRepository repository) : ICalculatorService
{
    public async Task<List<CalculatorHistory>> GetHistoryAsync()
    {
        return await repository.GetHistoryAsync();
    }

    public async Task<CalculateResult> CalculateTaxAsync(CalculateRequest calculationRequest)
    {
        return await repository.CalculateTaxAsync(calculationRequest);
    }
}