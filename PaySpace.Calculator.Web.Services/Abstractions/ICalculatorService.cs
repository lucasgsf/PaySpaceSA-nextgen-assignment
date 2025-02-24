using PaySpace.Calculator.Web.Borders.Models;

namespace PaySpace.Calculator.Web.Services.Abstractions
{
    public interface ICalculatorService
    {
        Task<List<CalculatorHistory>> GetHistoryAsync();

        Task<CalculateResult> CalculateTaxAsync(CalculateRequest calculationRequest);
    }
}