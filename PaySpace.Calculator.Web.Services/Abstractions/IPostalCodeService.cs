using PaySpace.Calculator.Web.Borders.Models;

namespace PaySpace.Calculator.Web.Services.Abstractions
{
    public interface IPostalCodeService
    {
        Task<List<PostalCode>> GetPostalCodesAsync();
    }
}