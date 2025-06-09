using SmartDrones.Domain.Entities;
using SmartDrones.Domain.Interfaces;
using SmartDrones.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDrones.Infrastructure.Repositories
{
    public class SensorDataRepository : ISensorDataRepository
    {
        private readonly SmartDronesDbContext _context;

        public SensorDataRepository(SmartDronesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SensorData>> GetAllAsync()
        {
            return await _context.SensorData.ToListAsync();
        }

        public async Task<SensorData?> GetByIdAsync(long id)
        {
            return await _context.SensorData.FirstOrDefaultAsync(sd => sd.Id == id);
        }

        public async Task<IEnumerable<SensorData>> GetByDroneIdAsync(long droneId)
        {
            return await _context.SensorData
                                 .Where(sd => sd.DroneId == droneId)
                                 .OrderByDescending(sd => sd.Timestamp)
                                 .ToListAsync();
        }

        public async Task AddAsync(SensorData sensorData)
        {
            await _context.SensorData.AddAsync(sensorData);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SensorData sensorData)
        {
            _context.SensorData.Update(sensorData);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(SensorData sensorData)
        {
            _context.SensorData.Remove(sensorData);
            await _context.SaveChangesAsync();
        }
    }
}