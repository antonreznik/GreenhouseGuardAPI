using Application.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceExtensions
    {
        extension(IServiceCollection services)
        {
            public void AddInfrastructureServices(IConfiguration configuration)
            {
                services.AddDbContext<SensorDbContext>(options =>
                    options.UseSqlite(configuration.GetConnectionString("SensorDb")));

                services.AddScoped<ISensorReadingRepository, SensorReadingRepository>();
            }
        }
    }
}
