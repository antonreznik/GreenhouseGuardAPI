using Application.Dto.Sensor;
using Application.Interfaces.Services;
using GreenhouseGuard.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GreenhouseGuard.Services
{
    public class ReadingNotifier(IHubContext<SensorHub> hubContext) : IReadingNotifier
    {
        public Task NotifyAsync(SensorReadingDto reading) =>
            hubContext.Clients.All.SendAsync("ReceiveReading", reading);
    }
}
