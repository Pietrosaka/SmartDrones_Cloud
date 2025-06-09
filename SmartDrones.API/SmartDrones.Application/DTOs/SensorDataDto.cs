using System;
using System.ComponentModel.DataAnnotations;

namespace SmartDrones.Application.DTOs
{
    public class SensorDataDto
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O ID do drone é obrigatório.")]
        public long DroneId { get; set; }

        [Required(ErrorMessage = "A temperatura é obrigatória.")]
        [Range(-50.0, 100.0, ErrorMessage = "A temperatura deve estar entre -50 e 100.")]
        public double Temperature { get; set; }

        [Required(ErrorMessage = "A umidade é obrigatória.")]
        [Range(0.0, 100.0, ErrorMessage = "A umidade deve estar entre 0 e 100.")]
        public double Humidity { get; set; }

        [Required(ErrorMessage = "A luminosidade é obrigatória.")]
        [Range(0.0, 2000.0, ErrorMessage = "A luminosidade deve estar entre 0 e 2000.")]
        public double Luminosity { get; set; }

        [Required(ErrorMessage = "A detecção de fumaça é obrigatória.")]
        public bool SmokeDetected { get; set; }

        [Required(ErrorMessage = "A latitude é obrigatória.")]
        [Range(-90.0, 90.0, ErrorMessage = "Latitude inválida.")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "A longitude é obrigatória.")]
        [Range(-180.0, 180.0, ErrorMessage = "Longitude inválida.")]
        public double Longitude { get; set; }
    }
}