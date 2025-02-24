using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Abstractions.Services
{
    public interface IPostalCodeService
    {
        Task<List<PostalCode>> GetPostalCodesAsync();
        Task<CalculatorType?> GetCalculatorTypeAsync(string code);
    }
}