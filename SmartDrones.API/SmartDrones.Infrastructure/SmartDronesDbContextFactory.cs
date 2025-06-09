using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using SmartDrones.Infrastructure.Data;

namespace SmartDrones.Infrastructure
{
    public class SmartDronesDbContextFactory : IDesignTimeDbContextFactory<SmartDronesDbContext>
    {
        public SmartDronesDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../SmartDrones.API")) // Aponta para a pasta do projeto API
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<SmartDronesDbContext>();
            optionsBuilder.UseOracle(connectionString, b =>
            {
                b.MigrationsAssembly(typeof(SmartDronesDbContext).Assembly.FullName);
            });

            return new SmartDronesDbContext(optionsBuilder.Options);
        }
    }
}