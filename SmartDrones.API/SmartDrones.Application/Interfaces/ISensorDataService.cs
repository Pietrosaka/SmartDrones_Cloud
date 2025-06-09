using SmartDrones.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDrones.Application.Interfaces
{
    public interface ISensorDataService
    {
        Task<IEnumerable<SensorDataDto>> GetAllSensorDataAsync();
        Task<SensorDataDto?> GetSensorDataByIdAsync(long id);
        Task<IEnumerable<SensorDataDto>> GetSensorDataByDroneIdAsync(long droneId);
        Task<SensorDataDto> AddSensorDataAsync(SensorDataDto sensorDataDto);
        Task<SensorDataDto> UpdateSensorDataAsync(SensorDataDto sensorDataDto);
        Task DeleteSensorDataAsync(long id);
    }
}