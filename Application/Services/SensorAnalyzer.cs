using Application.Dto.Enums;
using Application.Dto.Sensor;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.Services
{
    public class SensorAnalyzer(ISensorReadingStore sensorReadingStore) : ISensorAnalyzer
    {
        private const decimal ZScoreDangerThreshold = 2.5m;
        private const decimal ZScoreWarningThreshold = 2m;
        private const int CountOfItemsToAnalyze = 20;

        public async Task<List<ZscoreEvaluationDto>> AnalyzeAsync(SensorReading reading)
        {
            var recentReadings = sensorReadingStore.GetAll();

            var zScoreValues = EvaluateZScores(reading, recentReadings);

            return zScoreValues;
        }

        private static List<ZscoreEvaluationDto> EvaluateZScores(
            SensorReading reading,
            IEnumerable<SensorReading> recentReadings)
        {
            var zscoreTemperature = ZScore(reading.Temperature, recentReadings.Select(r => r.Temperature));
            var zscoreHumidity = ZScore(reading.Humidity, recentReadings.Select(r => r.Humidity));
            var zscoreCo2Ppm = ZScore(reading.Co2Ppm, recentReadings.Select(r => (decimal)r.Co2Ppm));

            var evaluationResult = new List<ZscoreEvaluationDto>()
            {
                new(SensorType.Temperature,
                    EvaluateSensorStatus(zscoreTemperature),
                    zscoreTemperature,
                    reading.Temperature),

                new(SensorType.Humidity,
                    EvaluateSensorStatus(zscoreHumidity),
                    zscoreHumidity,
                    reading.Humidity),

                new(SensorType.Co2Ppm,
                    EvaluateSensorStatus(zscoreCo2Ppm),
                    zscoreCo2Ppm,
                    reading.Co2Ppm),
            };

            return evaluationResult;
        }

        private static decimal ZScore(decimal value, IEnumerable<decimal> data)
        {
            if (data.Count() < CountOfItemsToAnalyze) 
            {
                return 0;            
            }

            var values = data.ToList();
            var mean = values.Average();
            var stdDev = (decimal)Math.Sqrt((double)values.Average(v => (v - mean) * (v - mean)));

            return stdDev == 0 ? 0 : (value - mean) / stdDev;
        }

        private static SensorStatus EvaluateSensorStatus(decimal zscore)
        {
            return Math.Abs(zscore) switch
            {
                > ZScoreDangerThreshold => SensorStatus.Danger,
                > ZScoreWarningThreshold => SensorStatus.Warning,
                _ => SensorStatus.Good
            };
        }
    }
}
