using SmartDrones.Web.Models;

namespace SmartDrones.Web.Services
{
    public interface IAlertApiService
    {
        Task<IEnumerable<AlertDto>?> GetAlertsAsync();
        Task<AlertDto?> GetAlertByIdAsync(long id);
        Task<AlertDto?> CreateAlertAsync(AlertDto alert);
        Task<AlertDto?> UpdateAlertAsync(long id, AlertDto alert);
        Task<bool> DeleteAlertAsync(long id);
    }
}