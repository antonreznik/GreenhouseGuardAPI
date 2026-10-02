using Application.Dto.Sensor;
using Application.Interfaces.Services;

namespace Application.RequestHandlers.Sensor.RecentAnomalies
{
    public class GetRecentAnomaliesHandler(IAnomalyStore anomalyStore) : IGetRecentAnomaliesHandler
    {
        public IReadOnlyCollection<AnomalyDto> Handle(int limit = 20)
        {
            var anomalies = anomalyStore.GetRecent(limit);

            return [.. anomalies
                .Select(a => new AnomalyDto(a.Id, a.DetectedAt, a.SensorType, a.Value, a.ZScore, a.Reason))];
        }
    }
}
