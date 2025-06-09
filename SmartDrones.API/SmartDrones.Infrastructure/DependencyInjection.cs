using Microsoft.Extensions.DependencyInjection;
using SmartDrones.Application.Interfaces;
using SmartDrones.Application.Services;
using SmartDrones.Domain.Interfaces;
using SmartDrones.Infrastructure.Data;
using SmartDrones.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using SmartDrones.Application.Mappings;

namespace SmartDrones.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SmartDronesDbContext>(options =>
                options.UseOracle(connectionString));
                
            services.AddScoped<IAlertRepository, AlertRepository>();
            services.AddScoped<IDroneRepository, DroneRepository>();
            services.AddScoped<ISensorDataRepository, SensorDataRepository>();

            services.AddScoped<IAlertService, AlertService>();
            services.AddScoped<IDroneService, DroneService>();
            services.AddScoped<ISensorDataService, SensorDataService>();

            services.AddAutoMapper(typeof(DomainToDtoMappingProfile).Assembly);

            return services;
        }
    }
}