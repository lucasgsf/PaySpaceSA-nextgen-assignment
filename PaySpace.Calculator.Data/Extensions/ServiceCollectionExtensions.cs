using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Contexts;
using PaySpace.Calculator.Data.Repositories;

namespace PaySpace.Calculator.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CalculatorContext>(opt =>
                opt.UseSqlite(configuration.GetConnectionString("CalculatorDatabase")));

            services.AddScoped<ICalculatorSettingsRepository, CalculatorSettingsRepository>();
            services.AddScoped<IHistoryRepository, HistoryRepository>();
            services.AddScoped<IPostalCodeRepository, PostalCodeRepository>();
        }

        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<CalculatorContext>();

            var pendingMigrations = context.Database.GetPendingMigrations().ToList();
            if (pendingMigrations.Any())
            {
                context.Database.Migrate();
            }
        }
    }
}