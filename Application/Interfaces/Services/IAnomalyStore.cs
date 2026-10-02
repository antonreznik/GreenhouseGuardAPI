using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IAnomalyStore
    {
        void Add(Anomaly anomaly);
        IReadOnlyCollection<Anomaly> GetRecent(int limit = 20);
    }
}
