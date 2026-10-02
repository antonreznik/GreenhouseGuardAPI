using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.Services
{
    // NOTE: This in-memory store is not thread-safe. It is implemented this way for demo purposes only.
    public class SensorReadingStore : ISensorReadingStore
    {
        private const int MaxReadings = 20;

        private readonly Queue<SensorReading> _readings = new();

        public int Count => _readings.Count;

        public void Add(SensorReading reading)
        {
            if (_readings.Count >= MaxReadings)
            {
                _readings.Dequeue();
            }

            _readings.Enqueue(reading);
        }

        public void AddRange(IEnumerable<SensorReading> readings)
        {
            foreach (var reading in readings)
            {
                Add(reading);
            }
        }

        public IReadOnlyCollection<SensorReading> GetAll()
        {
            return [.. _readings];
        }
    }
}
