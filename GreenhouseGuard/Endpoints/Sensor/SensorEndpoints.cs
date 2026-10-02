using Application.RequestHandlers.Sensor.CreateSensorReading;
using Application.RequestHandlers.Sensor.LatestSensorReading;
using Application.RequestHandlers.Sensor.RecentAnomalies;
using Application.RequestHandlers.Sensor.RecentReadings;

namespace GreenhouseGuard.Endpoints.Sensor
{
    public static class SensorEndpoints
    {
        extension(WebApplication app)
        {
            public void MapSensorEndpoints()
            {
                app.MapGet("/api/readings/latest", async (IGetLatestSensorReadingHandler latestSensorReading) =>
                {
                    var result = await latestSensorReading.HandleAsync();

                    return result is not null ? Results.Ok(result) : Results.NotFound();
                });

                app.MapGet("/api/readings/recent", (IGetRecentReadingsHandler getRecentReadingsHandler) =>
                {
                    var result = getRecentReadingsHandler.Handle();

                    return Results.Ok(result);
                });

                app.MapGet("/api/anomalies", (IGetRecentAnomaliesHandler getRecentAnomaliesHandler) =>
                {
                    var result = getRecentAnomaliesHandler.Handle();

                    return Results.Ok(result);
                });

                app.MapPost("/api/readings", async (ICreateSensorReadingHandler getOriginalUrlRequestHandler, CreateSensorReadingRequestData[] requestData) =>
                {
                    var result = await getOriginalUrlRequestHandler.CreateSensorReadingAsync(requestData);

                    return Results.Ok(result);
                });
            }

        }
    }
}

