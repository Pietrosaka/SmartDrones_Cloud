using System;
using SmartDrones.Domain.Enums;

namespace SmartDrones.Domain.Entities
{
    public class Alert
    {
        public long Id { get; }
        public long DroneId { get; private set; }
        public DateTime Timestamp { get; }
        public string Message { get; private set; } = null!;
        public RiskLevel RiskLevel { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public bool IsResolved { get; set; }

        public Drone Drone { get; private set; } = null!;

        private Alert() { }

        public Alert(long droneId, string message, RiskLevel riskLevel, double latitude, double longitude)
        {
            if (droneId <= 0)
                throw new ArgumentException("DroneId cannot be empty or zero.", nameof(droneId));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message cannot be null or empty.", nameof(message));

            DroneId = droneId;
            Message = message;
            RiskLevel = riskLevel;
            Latitude = latitude;
            Longitude = longitude;
            IsResolved = false;
        }

        public void ResolveAlert()
        {
            IsResolved = true;
        }

        public void UpdateMessage(string newMessage)
        {
            if (string.IsNullOrWhiteSpace(newMessage))
                throw new ArgumentException("Message cannot be null or empty.", nameof(newMessage));
            Message = newMessage;
        }

        public void UpdateRiskLevel(RiskLevel newRiskLevel)
        {
            RiskLevel = newRiskLevel;
        }
    }
}