using SmartDrones.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDrones.Domain.Interfaces
{
    public interface ISensorDataRepository
    {
        Task<IEnumerable<SensorData>> GetAllAsync();
        Task<SensorData?> GetByIdAsync(long id);
        Task<IEnumerable<SensorData>> GetByDroneIdAsync(long droneId);
        Task AddAsync(SensorData sensorData);
        Task UpdateAsync(SensorData sensorData);
        Task DeleteAsync(SensorData sensorData);
    }
}