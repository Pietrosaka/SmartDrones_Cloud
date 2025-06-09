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
    public class AlertRepository : IAlertRepository
    {
        private readonly SmartDronesDbContext _context;

        public AlertRepository(SmartDronesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Alert>> GetAllAsync()
        {
            return await _context.Alerts.Include(a => a.Drone).ToListAsync();
        }

        public async Task<Alert?> GetByIdAsync(long id)
        {
            return await _context.Alerts.Include(a => a.Drone).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Alert alert)
        {
            await _context.Alerts.AddAsync(alert);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Alert alert)
        {
            _context.Alerts.Update(alert);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Alert alert)
        {
            _context.Alerts.Remove(alert);
            await _context.SaveChangesAsync();
        }
    }
}