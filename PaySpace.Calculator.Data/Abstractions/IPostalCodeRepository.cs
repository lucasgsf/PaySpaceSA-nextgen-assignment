using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Data.Abstractions;

public interface IPostalCodeRepository
{
    Task<List<PostalCode>> GetPostalCodesAsync();
}