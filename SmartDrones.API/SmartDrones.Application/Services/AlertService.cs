using AutoMapper;
using SmartDrones.Application.DTOs;
using SmartDrones.Application.Interfaces;
using SmartDrones.Domain.Entities;
using SmartDrones.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDrones.Application.Services
{
    public class AlertService : IAlertService
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IMapper _mapper;

        public AlertService(IAlertRepository alertRepository, IMapper mapper)
        {
            _alertRepository = alertRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlertDto>> GetAllAlertsAsync()
        {
            var alerts = await _alertRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AlertDto>>(alerts);
        }

        public async Task<AlertDto?> GetAlertByIdAsync(long id)
        {
            var alert = await _alertRepository.GetByIdAsync(id);
            return _mapper.Map<AlertDto?>(alert);
        }

        public async Task<AlertDto> CreateAlertAsync(AlertDto alertDto)
        {
            var alert = _mapper.Map<Alert>(alertDto);
            await _alertRepository.AddAsync(alert);
            return _mapper.Map<AlertDto>(alert);
        }

        public async Task<AlertDto> UpdateAlertAsync(AlertDto alertDto)
        {
            var existingAlert = await _alertRepository.GetByIdAsync(alertDto.Id);
            if (existingAlert == null)
            {
                throw new ApplicationException($"Alerta com ID {alertDto.Id} não encontrado.");
            }

            _mapper.Map(alertDto, existingAlert);
            
            await _alertRepository.UpdateAsync(existingAlert);
            
            return _mapper.Map<AlertDto>(existingAlert);
        }

        public async Task<AlertDto> ResolveAlertAsync(long id)
        {
            var alert = await _alertRepository.GetByIdAsync(id);
            if (alert == null)
            {
                throw new ApplicationException($"Alerta com ID {id} não encontrado.");
            }

            alert.IsResolved = true; // Assuming Alert entity has an IsResolved property

            await _alertRepository.UpdateAsync(alert);

            return _mapper.Map<AlertDto>(alert);
        }

        public async Task DeleteAlertAsync(long id)
        {
            var existingAlert = await _alertRepository.GetByIdAsync(id);
            if (existingAlert == null)
            {
                throw new ApplicationException($"Alerta com ID {id} não encontrado para exclusão.");
            }
            await _alertRepository.DeleteAsync(existingAlert);
        }
    }
}