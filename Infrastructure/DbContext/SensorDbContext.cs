using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class SensorDbContext(DbContextOptions<SensorDbContext> options) : DbContext(options)
    {
        public DbSet<SensorReading> SensorReading => Set<SensorReading>();
        public DbSet<Anomaly> Anomalies => Set<Anomaly>();
    }
}

