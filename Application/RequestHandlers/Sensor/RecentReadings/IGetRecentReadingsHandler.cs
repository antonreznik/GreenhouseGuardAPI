using Application.Dto.Sensor;

namespace Application.RequestHandlers.Sensor.RecentReadings
{
    public interface IGetRecentReadingsHandler
    {
        IReadOnlyCollection<SensorReadingChartDto> Handle();
    }
}