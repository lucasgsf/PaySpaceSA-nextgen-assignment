using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Services;

namespace PaySpace.Calculator.Services.Services;

internal sealed class HistoryService(IHistoryRepository repository) : IHistoryService
{
    public async Task AddAsync(CalculatorHistory history)
    {
        history.Timestamp = DateTime.Now;
        await repository.AddAsync(history);
    }

    public async Task<List<CalculatorHistory>> GetHistoryAsync()
    {
        return await repository.GetHistoryAsync();
    }
}