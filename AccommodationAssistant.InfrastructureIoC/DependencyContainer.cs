using AccommodationAssistant.Application.Contracts;
using AccommodationAssistant.Application.Contracts.Interfaces;
using AccommodationAssistant.Persistence.Context;
using AccommodationAssistant.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AccommodationAssistant.InfrastructureIoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IContractService, ContractService>();
            
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IApartmentRepository, ApartmentRepository>();
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();

            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        public static async Task EnsureDatabaseMigratedAsync(this IServiceProvider provider)
        {
            var services = provider.CreateScope().ServiceProvider;
            var logger = services.GetRequiredService<ILogger<DataContext>>();

            try
            {
                using var context = services.GetRequiredService<DataContext>();

                await context.Database.MigrateAsync();
                await DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something error while initialize db: {ex.Message}", ex);
                throw;
            }
        }
    }
}