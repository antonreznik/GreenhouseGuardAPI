using Application.Interfaces.Services;
using Application.RequestHandlers.Sensor.CreateSensorReading;
using Application.RequestHandlers.Sensor.LatestSensorReading;
using Application.RequestHandlers.Sensor.RecentAnomalies;
using Application.RequestHandlers.Sensor.RecentReadings;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceExtensions
    {
        extension(IServiceCollection services)
        {
            public void AddApplicationServices()
            {
                services.AddScoped<ICreateSensorReadingHandler, CreateSensorReadingHandler>();
                services.AddScoped<IGetLatestSensorReadingHandler, GetLatestSensorReadingHandler>();
                services.AddScoped<IGetRecentAnomaliesHandler, GetRecentAnomaliesHandler>();
                services.AddScoped<IGetRecentReadingsHandler, GetRecentReadingsHandler>();
                services.AddScoped<ISensorAnalyzer, SensorAnalyzer>();
                services.AddSingleton<IAnomalyStore, AnomalyStore>();
                services.AddSingleton<ISensorReadingStore, SensorReadingStore>();
            }
        }
    }
}
