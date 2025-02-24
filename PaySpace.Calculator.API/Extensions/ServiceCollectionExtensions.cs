using Microsoft.Extensions.DependencyInjection.Extensions;
using PaySpace.Calculator.API.Abstractions;
using PaySpace.Calculator.API.Middlewares;

namespace PaySpace.Calculator.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddConverters(this IServiceCollection services)
    {
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IActionResultConverter, ActionResultConverter>();
    }
}