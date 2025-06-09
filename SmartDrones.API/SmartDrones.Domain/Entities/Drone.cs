using System;
using System.Collections.Generic;

namespace SmartDrones.Domain.Entities
{
    public class Drone
    {
        public long Id { get; }
        public string Identifier { get; private set; } = string.Empty;
        public string Model { get; private set; } = string.Empty;
        public string Status { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime LastActivity { get; private set; } 

        public ICollection<SensorData> SensorData { get; private set; }
        public ICollection<Alert> Alerts { get; private set; }
        public Drone(string identifier, string model)
        {
            Identifier = identifier;
            Model = model;
            Status = "Online";
            CreatedAt = DateTime.UtcNow;
            LastActivity = DateTime.UtcNow;

            SensorData = new List<SensorData>();
            Alerts = new List<Alert>();
        }

        protected Drone() 
        {
            SensorData = new List<SensorData>();
            Alerts = new List<Alert>();
        }

        public void UpdateDetails(string identifier, string model)
        {
            Identifier = identifier;
            Model = model;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateStatus(string status)
        {
            Status = status;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateLastActivity()
        {
            LastActivity = DateTime.UtcNow;
        }
    }
}