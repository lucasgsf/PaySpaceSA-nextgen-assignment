using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using PaySpace.Calculator.Borders.Models;
using PaySpace.Calculator.Borders.Validators;
using System.Reflection;

namespace PaySpace.Calculator.Borders.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddExtensions(this IServiceCollection services, Assembly assembly)
    {
        TypeAdapterConfig.GlobalSettings.Scan(assembly, typeof(ServiceCollectionExtensions).Assembly);

        services.AddScoped<IValidator<CalculateRequest>, CalculateRequestValidator>();
    }
}