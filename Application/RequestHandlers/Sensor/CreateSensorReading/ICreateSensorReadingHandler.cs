using Application.Dto.Sensor;

namespace Application.RequestHandlers.Sensor.CreateSensorReading
{
    public interface ICreateSensorReadingHandler
    {
        Task<List<SensorReadingDto>> CreateSensorReadingAsync(CreateSensorReadingRequestData[] data);
    }
}
