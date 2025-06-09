namespace SmartDrones.Web.Models
{
    public class SensorDataDto
    {
        public long Id { get; set; }
        public long DroneId { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Luminosity { get; set; }
        public bool SmokeDetected { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}