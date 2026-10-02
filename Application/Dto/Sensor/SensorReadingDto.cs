namespace Application.Dto.Sensor
{
    public record SensorReadingDto(Guid Id,
        List<ZscoreEvaluationDto> ZscoreEvaluationDtos,
        DateTime Timestamp
        );
}
