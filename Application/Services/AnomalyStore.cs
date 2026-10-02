using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.Services
{
    // NOTE: This in-memory store is not thread-safe. It is implemented this way for demo purposes only.
    public class AnomalyStore : IAnomalyStore
    {
        private readonly List<Anomaly> _anomalies = [];

        public void Add(Anomaly anomaly) => _anomalies.Add(anomaly);

        public IReadOnlyCollection<Anomaly> GetRecent(int limit = 20) => [.. _anomalies
            .OrderByDescending(x => x.DetectedAt)
            .Take(limit)];
    }
}
