using Application.Dto.Sensor;

namespace Application.RequestHandlers.Sensor.LatestSensorReading
{
    public interface IGetLatestSensorReadingHandler
    {
        Task<SensorReadingDto?> HandleAsync();
    }
}
