using SmartDrones.Web.Models;

namespace SmartDrones.Web.Services
{
    public interface ISensorDataApiService
    {
        Task<IEnumerable<SensorDataDto>?> GetSensorDataAsync();
        Task<SensorDataDto?> GetSensorDataByIdAsync(long id);
        Task<SensorDataDto?> CreateSensorDataAsync(SensorDataDto sensorData);
        Task<SensorDataDto?> UpdateSensorDataAsync(long id, SensorDataDto sensorData);
        Task<bool> DeleteSensorDataAsync(long id);
    }
}