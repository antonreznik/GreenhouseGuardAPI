using Application.Dto.Sensor;

namespace Application.Interfaces.Services
{
    public interface IAnomalyNotifier
    {
        Task NotifyAsync(AnomalyDto anomaly);
    }
}
