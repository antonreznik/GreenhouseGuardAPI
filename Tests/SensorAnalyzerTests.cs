using Application.Dto.Enums;
using Application.Interfaces.Services;
using Application.Services;
using Domain.Entities;
using Moq;

namespace Tests;

public class SensorAnalyzerTests
{
    [Fact]
    public async Task AnalyzeAsync_WithFewerThanTwentyReadings_ReturnsGoodForAllSensors()
    {
        var store = CreateStore(CreateReadings(19, 25m, 60m, 400));
        var analyzer = new SensorAnalyzer(store.Object);

        var evaluations = await analyzer.AnalyzeAsync(CreateReading(100m, 10m, 1000));

        Assert.Equal(3, evaluations.Count);
        Assert.All(evaluations, evaluation =>
        {
            Assert.Equal(SensorStatus.Good, evaluation.SensorStatus);
            Assert.Equal(0m, evaluation.ZScore);
        });
    }

    [Fact]
    public async Task AnalyzeAsync_WithConstantBaseline_ReturnsGoodForAllSensors()
    {
        var store = CreateStore(CreateReadings(20, 25m, 60m, 400));
        var analyzer = new SensorAnalyzer(store.Object);

        var evaluations = await analyzer.AnalyzeAsync(CreateReading(25m, 60m, 400));

        Assert.All(evaluations, evaluation =>
        {
            Assert.Equal(SensorStatus.Good, evaluation.SensorStatus);
            Assert.Equal(0m, evaluation.ZScore);
        });
    }

    [Fact]
    public async Task AnalyzeAsync_WithHighTemperature_ReturnsDangerForTemperature()
    {
        var store = CreateStore(CreateVariableReadings());
        var analyzer = new SensorAnalyzer(store.Object);

        var evaluations = await analyzer.AnalyzeAsync(CreateReading(40m, 60m, 400));

        var temperature = Assert.Single(evaluations, evaluation => evaluation.SensorType == SensorType.Temperature);
        Assert.Equal(SensorStatus.Danger, temperature.SensorStatus);
        Assert.True(temperature.ZScore > 2.5m);
    }

    [Fact]
    public async Task AnalyzeAsync_WithLowHumidity_ReturnsDangerForHumidity()
    {
        var store = CreateStore(CreateVariableReadings());
        var analyzer = new SensorAnalyzer(store.Object);

        var evaluations = await analyzer.AnalyzeAsync(CreateReading(25m, 20m, 400));

        var humidity = Assert.Single(evaluations, evaluation => evaluation.SensorType == SensorType.Humidity);
        Assert.Equal(SensorStatus.Danger, humidity.SensorStatus);
        Assert.True(humidity.ZScore < -2.5m);
    }

    private static Mock<ISensorReadingStore> CreateStore(IEnumerable<SensorReading> readings)
    {
        var store = new Mock<ISensorReadingStore>();
        store.Setup(s => s.GetAll()).Returns(readings.ToList());
        return store;
    }

    private static List<SensorReading> CreateReadings(int count, decimal temperature, decimal humidity, int co2Ppm) =>
        Enumerable.Range(0, count)
            .Select(index => CreateReading(temperature, humidity, co2Ppm, index))
            .ToList();

    private static List<SensorReading> CreateVariableReadings() =>
        Enumerable.Range(0, 20)
            .Select(index => CreateReading(
                24m + index % 3,
                59m + index % 3,
                390 + index % 3 * 5,
                index))
            .ToList();

    private static SensorReading CreateReading(decimal temperature, decimal humidity, int co2Ppm, long sequenceNumber = 1) =>
        new()
        {
            Id = Guid.NewGuid(),
            SequenceNumber = sequenceNumber,
            Timestamp = DateTime.UtcNow,
            Temperature = temperature,
            Humidity = humidity,
            Co2Ppm = co2Ppm
        };
}
