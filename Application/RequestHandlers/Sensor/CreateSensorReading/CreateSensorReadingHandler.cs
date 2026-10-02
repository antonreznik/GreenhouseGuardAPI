using Application.Dto.Enums;
using Application.Dto.Sensor;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.RequestHandlers.Sensor.CreateSensorReading
{
    public class CreateSensorReadingHandler(
        ISensorReadingRepository sensorReadingRepository, 
        ISensorAnalyzer anomalyDetector, 
        IReadingNotifier readingNotifier,
        IAnomalyStore anomalyStore, 
        ISensorReadingStore sensorReadingStore,
        IAnomalyNotifier anomalyNotifier) : ICreateSensorReadingHandler
    {
        public async Task<List<SensorReadingDto>> CreateSensorReadingAsync(CreateSensorReadingRequestData[] data)
        {
            var readings = data.Select(d => new SensorReading
            {
                Id = Guid.NewGuid(),
                SequenceNumber = d.SequenceNumber,
                Timestamp = d.Timestamp,
                Temperature = d.Temperature,
                Humidity = d.Humidity,
                Co2Ppm = d.Co2Ppm
            }).ToList();

            var createdReadings = await sensorReadingRepository.CreateSensorReadingsAsync(readings);

            List<SensorReadingDto> responseDtos = [];

            foreach (var reading in createdReadings.OrderBy(r => r.SequenceNumber))
            {
                var zscoreEvaluations = await anomalyDetector.AnalyzeAsync(reading);

                await CheckForAnomaliesAsync(zscoreEvaluations, reading);

                await readingNotifier.NotifyAsync(new SensorReadingDto(reading.Id, zscoreEvaluations, reading.Timestamp));

                responseDtos.Add(new(reading.Id,
                    zscoreEvaluations,
                    reading.Timestamp));
            }

            sensorReadingStore.AddRange(readings);

            return responseDtos;
        }

        private Task CheckForAnomaliesAsync(List<ZscoreEvaluationDto> zscoreEvaluations, SensorReading reading)
        {
            var anomalies = zscoreEvaluations
                .Where(s => s.SensorStatus == SensorStatus.Danger)
                .Select(s => CreateAnomalyAsync(s, reading.Timestamp));

            return Task.WhenAll(anomalies);
        }

        private async Task CreateAnomalyAsync(ZscoreEvaluationDto zscoreEvaluation, DateTime timeStamp)
        {
            var anomaly = new Anomaly
            {
                Id = Guid.NewGuid(),
                DetectedAt = timeStamp,
                SensorType = zscoreEvaluation.SensorType.ToString(),
                Value = zscoreEvaluation.OriginalValue,
                ZScore = zscoreEvaluation.ZScore,
                Reason = zscoreEvaluation.ZScore < 0
                    ? $"{zscoreEvaluation.SensorType.ToString()} is too low"
                    : $"{zscoreEvaluation.SensorType.ToString()} is too high"
            };

            anomalyStore.Add(anomaly);

            await anomalyNotifier.NotifyAsync(new AnomalyDto(anomaly.Id, anomaly.DetectedAt, anomaly.SensorType, anomaly.Value, anomaly.ZScore, anomaly.Reason));
        }
    }
}
