using Microsoft.EntityFrameworkCore;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Contexts;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Data.Repositories;

internal sealed class CalculatorSettingsRepository(CalculatorContext _context) : ICalculatorSettingsRepository
{
    public async Task<List<CalculatorSetting>> GetSettingsAsync(CalculatorType calculatorType)
    {
        return await _context.Set<CalculatorSetting>()
                                .AsNoTracking()
                                .Where(_ => _.Calculator == calculatorType)
                                .ToListAsync();
    }
}