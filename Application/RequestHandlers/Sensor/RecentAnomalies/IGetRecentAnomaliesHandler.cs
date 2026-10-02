using Application.Dto.Sensor;

namespace Application.RequestHandlers.Sensor.RecentAnomalies
{
    public interface IGetRecentAnomaliesHandler
    {
        IReadOnlyCollection<AnomalyDto> Handle(int limit = 20);
    }
}
