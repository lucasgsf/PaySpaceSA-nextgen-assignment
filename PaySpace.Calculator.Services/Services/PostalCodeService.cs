using Microsoft.Extensions.Caching.Memory;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Services;

namespace PaySpace.Calculator.Services.Services
{
    internal sealed class PostalCodeService(IPostalCodeRepository repository, IMemoryCache memoryCache) : IPostalCodeService
    {
        public async Task<List<PostalCode>> GetPostalCodesAsync()
        {
            return await memoryCache.GetOrCreateAsync("PostalCodes", async _ => await repository.GetPostalCodesAsync())!;
        }

        public async Task<CalculatorType?> GetCalculatorTypeAsync(string code)
        {
            var postalCodes = await GetPostalCodesAsync();

            var postalCode = postalCodes.FirstOrDefault(pc => pc.Code == code);

            return postalCode?.Calculator;
        }
    }
}