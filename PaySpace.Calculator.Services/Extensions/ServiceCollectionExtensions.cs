using Microsoft.Extensions.DependencyInjection;
using PaySpace.Calculator.Services.Abstractions.Builders;
using PaySpace.Calculator.Services.Abstractions.Calculators;
using PaySpace.Calculator.Services.Abstractions.Services;
using PaySpace.Calculator.Services.Abstractions.Strategies;
using PaySpace.Calculator.Services.Abstractions.UseCases;
using PaySpace.Calculator.Services.Builders;
using PaySpace.Calculator.Services.Calculators;
using PaySpace.Calculator.Services.Services;
using PaySpace.Calculator.Services.Strategies;
using PaySpace.Calculator.Services.UseCases;

namespace PaySpace.Calculator.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICalculationService, AmountValueCalculationService>();
            services.AddScoped<ICalculationService, PercentageValueCalculationService>();

            services.AddScoped<ICalculatorSettingsService, CalculatorSettingsService>();
            services.AddScoped<IHistoryService, HistoryService>();
            services.AddScoped<IPostalCodeService, PostalCodeService>();

            services.AddScoped<ITaxCalculator, FlatRateCalculator>();
            services.AddScoped<ITaxCalculator, FlatValueCalculator>();
            services.AddScoped<ITaxCalculator, ProgressiveCalculator>();

            services.AddTransient<ICalculateResultBuilder, CalculateResultBuilder>();

            services.AddScoped<ICalculationServiceStrategy, CalculationServiceStrategy>();
            services.AddScoped<ITaxCalculatorStrategy, TaxCalculatorStrategy>();

            services.AddScoped<ICalculateTaxUseCase, CalculateTaxUseCase>();
            services.AddScoped<IGetCalculatorHistoryUseCase, GetCalculatorHistoryUseCase>();
            services.AddScoped<IGetPostalCodesUseCase, GetPostalCodesUseCase>();

            services.AddMemoryCache();
        }
    }
}