using Microsoft.EntityFrameworkCore;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Contexts;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Data.Repositories;

internal sealed class HistoryRepository(CalculatorContext _context) : IHistoryRepository
{
    public async Task AddAsync(CalculatorHistory calculatorHistory)
    {
        await _context.Set<CalculatorHistory>().AddAsync(calculatorHistory);
        await _context.SaveChangesAsync();
    }

    public Task<List<CalculatorHistory>> GetHistoryAsync()
    {
        return _context.Set<CalculatorHistory>()
                        .OrderByDescending(_ => _.Timestamp)
                        .ToListAsync();
    }
}