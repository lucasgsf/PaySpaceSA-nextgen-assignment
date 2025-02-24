using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Data.Abstractions;

public interface IHistoryRepository
{
    Task<List<CalculatorHistory>> GetHistoryAsync();
    Task AddAsync(CalculatorHistory calculatorHistory);
}