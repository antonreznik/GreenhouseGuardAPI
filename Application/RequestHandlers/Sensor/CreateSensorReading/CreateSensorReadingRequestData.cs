namespace Application.RequestHandlers.Sensor.CreateSensorReading
{
    public record CreateSensorReadingRequestData(long SequenceNumber, 
        DateTime Timestamp, 
        decimal Temperature, 
        decimal Humidity, 
        int Co2Ppm);
}
