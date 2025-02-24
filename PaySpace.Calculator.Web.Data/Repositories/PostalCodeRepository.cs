using PaySpace.Calculator.Web.Borders.Models;
using PaySpace.Calculator.Web.Data.Abstractions;
using PaySpace.Calculator.Web.Data.Helpers;

namespace PaySpace.Calculator.Web.Data.Repositories;

internal sealed class PostalCodeRepository(HttpClient httpClient) : IPostalCodeRepository
{
    public async Task<List<PostalCode>> GetPostalCodesAsync()
    {
        var requestUri = "/api/PostalCode";
        return await ApiHelper.Get<List<PostalCode>>(requestUri, httpClient);
    }
}