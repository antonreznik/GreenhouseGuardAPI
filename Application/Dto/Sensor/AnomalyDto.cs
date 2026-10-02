namespace Application.Dto.Sensor
{
    public record AnomalyDto(Guid Id,
        DateTime DetectedAt,
        string SensorType,
        decimal Value,
        decimal ZScore,
        string Reason);
}
