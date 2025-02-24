using PaySpace.Calculator.Web.Borders.Models;
using PaySpace.Calculator.Web.Data.Abstractions;
using PaySpace.Calculator.Web.Services.Abstractions;

namespace PaySpace.Calculator.Web.Services.Services;

public class PostalCodeService(IPostalCodeRepository repository) : IPostalCodeService
{
    public async Task<List<PostalCode>> GetPostalCodesAsync()
    {
        return await repository.GetPostalCodesAsync();
    }
}