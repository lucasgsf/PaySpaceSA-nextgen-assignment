using Microsoft.EntityFrameworkCore;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Contexts;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Data.Repositories;

internal sealed class PostalCodeRepository(CalculatorContext _context) : IPostalCodeRepository
{
    public async Task<List<PostalCode>> GetPostalCodesAsync()
    {
        return await _context.Set<PostalCode>()
                                .AsNoTracking()
                                .ToListAsync();
    }
}