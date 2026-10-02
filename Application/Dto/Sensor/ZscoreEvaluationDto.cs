using Application.Dto.Enums;

namespace Application.Dto.Sensor
{
    public record ZscoreEvaluationDto(SensorType SensorType, 
        SensorStatus SensorStatus, 
        decimal ZScore, 
        decimal OriginalValue);
}
