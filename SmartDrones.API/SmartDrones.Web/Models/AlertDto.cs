using System;

namespace SmartDrones.Web.Models
{
    public enum RiskLevel
    {
        Low,
        Medium,
        High,
        Critical
    }

    public class AlertDto
    {
        public long Id { get; set; }
        public long DroneId { get; set; }
        public DateTime? Timestamp { get; set; }
        public string Message { get; set; } = string.Empty;
        public RiskLevel RiskLevel { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsResolved { get; set; }
    }
}