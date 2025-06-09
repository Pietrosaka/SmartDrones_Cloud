using System;
using System.ComponentModel.DataAnnotations;

namespace SmartDrones.Application.DTOs
{
    public class DroneDto
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O identificador é obrigatório.")]
        [StringLength(50, ErrorMessage = "O identificador deve ter no máximo 50 caracteres.")]
        public string Identifier { get; set; } = string.Empty;

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        [StringLength(100, ErrorMessage = "O modelo deve ter no máximo 100 caracteres.")]
        public string Model { get; set; } = string.Empty;

        public string? Status { get; set; }
    }
}