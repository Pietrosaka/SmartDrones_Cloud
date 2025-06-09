using SmartDrones.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDrones.Domain.Interfaces
{
    public interface IAlertRepository
    {
        Task<IEnumerable<Alert>> GetAllAsync();
        Task<Alert?> GetByIdAsync(long id);
        Task AddAsync(Alert alert);
        Task UpdateAsync(Alert alert);
        Task DeleteAsync(Alert alert);
    }
}