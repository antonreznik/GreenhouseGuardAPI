using Application.Dto.Sensor;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface ISensorAnalyzer
    {
        Task<List<ZscoreEvaluationDto>> AnalyzeAsync(SensorReading reading);
    }
}
