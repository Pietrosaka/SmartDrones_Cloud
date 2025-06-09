using SmartDrones.Web.Models;

namespace SmartDrones.Web.Services
{
    public interface IDroneApiService
    {
        Task<IEnumerable<DroneDto>?> GetDronesAsync();
        Task<DroneDto?> GetDroneByIdAsync(long id);
        Task<DroneDto?> CreateDroneAsync(DroneDto drone);
        Task<DroneDto?> UpdateDroneAsync(long id, DroneDto drone);
        Task<bool> DeleteDroneAsync(long id);
    }
}