using Microsoft.Extensions.Caching.Memory;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Services;

namespace PaySpace.Calculator.Services.Services;

internal sealed class CalculatorSettingsService(ICalculatorSettingsRepository _repository, IMemoryCache memoryCache) : ICalculatorSettingsService
{
    public async Task<List<CalculatorSetting>> GetSettingsAsync(CalculatorType calculatorType)
    {
        return await memoryCache.GetOrCreateAsync($"CalculatorSetting: {calculatorType}", async entry =>
        {
            return (await _repository.GetSettingsAsync(calculatorType)).OrderBy(_ => _.From).ToList();
        })!;
    }

    public async Task<CalculatorSetting> GetSettingAsync(CalculatorType calculatorType, decimal income)
    {
        var lstSettings = await this.GetSettingsAsync(calculatorType);

        var setting = lstSettings.FirstOrDefault(_ => _.From <= income && (_.To == null || _.To >= income));

        if (setting != null)
            return setting;

        //throw new ServiceException(ErrorMessages.CalculatorSettingsNotFound, HttpStatusCode.NotFound);
        return new()
        {
            Calculator = calculatorType,
            Rate = 0,
        };
    }
}