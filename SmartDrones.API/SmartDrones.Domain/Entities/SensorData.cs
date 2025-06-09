using System;

namespace SmartDrones.Domain.Entities
{
    public class SensorData
    {
        public long Id { get; }
        public long DroneId { get; private set; }
        public double Temperature { get; private set; }
        public double Humidity { get; private set; }
        public double Luminosity { get; private set; }
        public bool SmokeDetected { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public DateTime Timestamp { get; private set; }

        public Drone? Drone { get; }

        public SensorData(long droneId, double temperature, double humidity, double luminosity, bool smokeDetected, double latitude, double longitude)
        {
            DroneId = droneId;
            Temperature = temperature;
            Humidity = humidity;
            Luminosity = luminosity;
            SmokeDetected = smokeDetected;
            Latitude = latitude;
            Longitude = longitude;
            Timestamp = DateTime.UtcNow;
        }

        protected SensorData() { } 

        public void UpdateLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public void UpdateSensorReadings(double temperature, double humidity, double luminosity, bool smokeDetected)
        {
            Temperature = temperature;
            Humidity = humidity;
            Luminosity = luminosity;
            SmokeDetected = smokeDetected;
        }
    }
}