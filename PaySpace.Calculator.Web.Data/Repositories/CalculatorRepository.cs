using PaySpace.Calculator.Web.Borders.Models;
using PaySpace.Calculator.Web.Data.Abstractions;
using PaySpace.Calculator.Web.Data.Helpers;

namespace PaySpace.Calculator.Web.Data.Repositories;

internal sealed class CalculatorRepository(HttpClient httpClient) : ICalculatorRepository
{
    public async Task<CalculateResult> CalculateTaxAsync(CalculateRequest calculationRequest)
    {
        var requestUri = "/api/Calculator/calculate-tax";
        return await ApiHelper.Post<CalculateResult>(requestUri, httpClient, calculationRequest);
    }

    public async Task<List<CalculatorHistory>> GetHistoryAsync()
    {
        var requestUri = "/api/Calculator/history";
        return await ApiHelper.Get<List<CalculatorHistory>>(requestUri, httpClient);
    }
}