using PaySpace.Calculator.Web.Borders.Models;

namespace PaySpace.Calculator.Web.Data.Abstractions;

public interface ICalculatorRepository
{
    Task<List<CalculatorHistory>> GetHistoryAsync();
    Task<CalculateResult> CalculateTaxAsync(CalculateRequest calculationRequest);
}