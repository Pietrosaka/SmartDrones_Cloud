using SmartDrones.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDrones.Application.Interfaces
{
    public interface IDroneService
    {
        Task<IEnumerable<DroneDto>> GetAllDronesAsync();
        Task<DroneDto?> GetDroneByIdAsync(long id);
        Task<DroneDto> CreateDroneAsync(DroneDto droneDto);
        Task<DroneDto> UpdateDroneAsync(DroneDto droneDto);
        Task DeleteDroneAsync(long id);
    }
}