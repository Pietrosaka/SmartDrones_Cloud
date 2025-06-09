using SmartDrones.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDrones.Application.Interfaces
{
    public interface IAlertService
    {
        Task<IEnumerable<AlertDto>> GetAllAlertsAsync();
        Task<AlertDto?> GetAlertByIdAsync(long id);
        Task<AlertDto> CreateAlertAsync(AlertDto alertDto);
        Task<AlertDto> UpdateAlertAsync(AlertDto alertDto);
        Task DeleteAlertAsync(long id);
        Task<AlertDto> ResolveAlertAsync(long id);
    }
}