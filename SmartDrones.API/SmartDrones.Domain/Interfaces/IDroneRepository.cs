using SmartDrones.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDrones.Domain.Interfaces
{
    public interface IDroneRepository
    {
        Task<IEnumerable<Drone>> GetAllAsync();
        Task<Drone?> GetByIdAsync(long id);
        Task AddAsync(Drone drone);
        Task UpdateAsync(Drone drone);
        Task DeleteAsync(Drone drone);
    }
}