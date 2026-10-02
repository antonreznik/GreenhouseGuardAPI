using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface ISensorReadingStore
    {
        void Add(SensorReading reading);

        void AddRange(IEnumerable<SensorReading> readings);

        IReadOnlyCollection<SensorReading> GetAll();

        int Count { get; }
    }
}
