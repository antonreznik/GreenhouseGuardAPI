using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ISensorReadingRepository
    {
        Task<SensorReading?> GetLatestSensorReadingAsync();
        Task<List<Anomaly>> GetRecentAnomaliesAsync(int limit = 10);
        Task<IEnumerable<SensorReading>> CreateSensorReadingsAsync(IReadOnlyCollection<SensorReading> readings);
        Task<List<SensorReading>> GetRecentReadingsAsync(int limit = 20);
    }
}

