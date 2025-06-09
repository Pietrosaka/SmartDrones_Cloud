using System;
using System.ComponentModel.DataAnnotations;
using SmartDrones.Domain.Enums; 

namespace SmartDrones.Application.DTOs
{
    public class AlertDto
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O ID do drone é obrigatório.")]
        public long DroneId { get; set; }
        public DateTime? Timestamp { get; set; }

        [Required(ErrorMessage = "A mensagem do alerta é obrigatória.")]
        [StringLength(500, ErrorMessage = "A mensagem deve ter no máximo 500 caracteres.")]
        public string Message { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nível de risco é obrigatório.")]
        [EnumDataType(typeof(RiskLevel), ErrorMessage = "Nível de risco inválido.")]
        public RiskLevel RiskLevel { get; set; }

        [Required(ErrorMessage = "A latitude é obrigatória.")]
        [Range(-90.0, 90.0, ErrorMessage = "Latitude inválida.")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "A longitude é obrigatória.")]
        [Range(-180.0, 180.0, ErrorMessage = "Longitude inválida.")]
        public double Longitude { get; set; }

        public bool IsResolved { get; set; }
    }
}