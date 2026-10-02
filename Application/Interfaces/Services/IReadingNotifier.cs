using Application.Dto.Sensor;

namespace Application.Interfaces.Services
{
    public interface IReadingNotifier
    {
        Task NotifyAsync(SensorReadingDto reading);
    }
}
