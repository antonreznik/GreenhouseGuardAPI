
using Application.Dto.Sensor;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;

namespace Application.RequestHandlers.Sensor.LatestSensorReading
{
    public class GetLatestSensorReadingHandler(
        ISensorReadingRepository sensorReadingRepository, 
        ISensorAnalyzer sensorAnalyzer) : IGetLatestSensorReadingHandler
    {
        public async Task<SensorReadingDto?> HandleAsync()
        {
            var domain = await sensorReadingRepository.GetLatestSensorReadingAsync();

            return domain is not null
                ? new SensorReadingDto(domain.Id, await sensorAnalyzer.AnalyzeAsync(domain), domain.Timestamp)
                : null;
        }
    }
}

