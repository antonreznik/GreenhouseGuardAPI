using Application.Dto.Sensor;
using Application.Interfaces.Services;
using GreenhouseGuard.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GreenhouseGuard.Services
{
    public class AnomalyNotifier(IHubContext<SensorHub> hubContext) : IAnomalyNotifier
    {
        public Task NotifyAsync(AnomalyDto anomaly) =>
            hubContext.Clients.All.SendAsync("ReceiveAnomaly", anomaly);
    }
}
