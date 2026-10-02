using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SensorReadingRepository(SensorDbContext sensorDbContext) : ISensorReadingRepository
    {
        public async Task<IEnumerable<SensorReading>> CreateSensorReadingsAsync(IReadOnlyCollection<SensorReading> readings)
        {
            if (readings.Count == 0)
            {
                return [];
            }

            sensorDbContext.SensorReading.AddRange(readings);
            await sensorDbContext.SaveChangesAsync();

            return readings;
        }

        public Task<SensorReading?> GetLatestSensorReadingAsync()
        {
            return sensorDbContext.SensorReading
                .AsNoTracking()
                .OrderByDescending(x => x.Timestamp)
                .FirstOrDefaultAsync();
        }

        public Task<List<SensorReading>> GetRecentReadingsAsync(int limit = 20)
        {
            return sensorDbContext.SensorReading
                .AsNoTracking()
                .OrderByDescending(x => x.Timestamp)  
                .Take(limit)
                .ToListAsync();
        }

        public Task<List<Anomaly>> GetRecentAnomaliesAsync(int limit = 10)
        {
            return sensorDbContext.Anomalies
               .AsNoTracking()
               .OrderByDescending(x => x.DetectedAt)
               .Take(limit)
               .ToListAsync();
        }
    }
}

