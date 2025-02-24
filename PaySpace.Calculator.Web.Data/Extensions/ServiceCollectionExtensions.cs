using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaySpace.Calculator.Web.Data.Abstractions;
using PaySpace.Calculator.Web.Data.Repositories;

namespace PaySpace.Calculator.Web.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<ICalculatorRepository, CalculatorRepository>(c =>
        {
            c.BaseAddress = new(configuration.GetValue<string>("CalculatorSettings:ApiUrl"));
            c.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        services.AddHttpClient<IPostalCodeRepository, PostalCodeRepository>(c =>
        {
            c.BaseAddress = new(configuration.GetValue<string>("CalculatorSettings:ApiUrl"));
            c.DefaultRequestHeaders.Add("Accept", "application/json");
        });
    }
}