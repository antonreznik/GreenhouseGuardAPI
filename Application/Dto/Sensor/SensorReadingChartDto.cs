namespace Application.Dto.Sensor
{
    public record SensorReadingChartDto(
        Guid Id,
        DateTime Timestamp,
        decimal Temperature,
        decimal Humidity,
        int Co2Ppm);
}