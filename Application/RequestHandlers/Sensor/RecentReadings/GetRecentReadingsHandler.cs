using Application.Dto.Sensor;
using Application.Interfaces.Services;

namespace Application.RequestHandlers.Sensor.RecentReadings
{
    public class GetRecentReadingsHandler(ISensorReadingStore sensorReadingStore) : IGetRecentReadingsHandler
    {
        public IReadOnlyCollection<SensorReadingChartDto> Handle()
        {
            var readings = sensorReadingStore.GetAll();

            var dtos = readings.Select(reading =>
                new SensorReadingChartDto(reading.Id,reading.Timestamp, reading.Temperature, reading.Humidity, reading.Co2Ppm));

            return [.. dtos];
        }
    }
}