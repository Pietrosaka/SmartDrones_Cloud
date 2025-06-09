namespace SmartDrones.Web.Models
{
    public class DroneDto
    {
        public long Id { get; set; }
        public string Identifier { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string? Status { get; set; }
    }
}