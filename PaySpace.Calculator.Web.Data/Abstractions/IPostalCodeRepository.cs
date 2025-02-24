using PaySpace.Calculator.Web.Borders.Models;

namespace PaySpace.Calculator.Web.Data.Abstractions;

public interface IPostalCodeRepository
{
    Task<List<PostalCode>> GetPostalCodesAsync();
}