using Application.Interfaces.Services;
using GreenhouseGuard.Middleware;
using GreenhouseGuard.Services;
using System.Text.Json.Serialization;

namespace GreenhouseGuard
{
    public static class ServiceExtensions
    {
        extension(IServiceCollection services)
        {
            public void AddApiServices()
            {
                services.AddCors(options =>
                {
                    options.AddPolicy("Angular", policy =>
                    {
                        policy
                            .WithOrigins("http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
                });

                services.AddOpenApi();
                services.AddExceptionHandler<GlobalExceptionHandler>();
                services.AddProblemDetails();
                services.AddScoped<IAnomalyNotifier, AnomalyNotifier>();
                services.AddScoped<IReadingNotifier, ReadingNotifier>();

                services.AddSignalR()
                    .AddJsonProtocol(options =>
                    {
                        options.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    });

                services.ConfigureHttpJsonOptions(options =>
                {
                    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            }
        }
    }
}
