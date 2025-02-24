using Microsoft.Extensions.DependencyInjection;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Services;

namespace PaySpace.Calculator.Web.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCalculatorHttpServices(this IServiceCollection services)
    {
        services.AddScoped<ICalculatorService, CalculatorService>();
        services.AddScoped<IPostalCodeService, PostalCodeService>();
    }
}