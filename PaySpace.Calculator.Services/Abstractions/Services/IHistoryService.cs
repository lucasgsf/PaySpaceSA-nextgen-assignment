using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Abstractions.Services
{
    public interface IHistoryService
    {
        Task<List<CalculatorHistory>> GetHistoryAsync();

        Task AddAsync(CalculatorHistory calculatorHistory);
    }
}