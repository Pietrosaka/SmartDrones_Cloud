using SmartDrones.Application.DTOs;
using SmartDrones.Application.Interfaces;
using SmartDrones.Domain.Entities;
using SmartDrones.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDrones.Application.Services
{
    public class DroneService : IDroneService
    {
        private readonly IDroneRepository _droneRepository;
        private readonly IMapper _mapper;

        public DroneService(IDroneRepository droneRepository, IMapper mapper)
        {
            _droneRepository = droneRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DroneDto>> GetAllDronesAsync()
        {
            var drones = await _droneRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DroneDto>>(drones);
        }

        public async Task<DroneDto?> GetDroneByIdAsync(long id)
        {
            var drone = await _droneRepository.GetByIdAsync(id);
            return _mapper.Map<DroneDto>(drone);
        }

        public async Task<DroneDto> CreateDroneAsync(DroneDto droneDto)
        {
            var drone = new Drone(droneDto.Identifier, droneDto.Model);
            drone.UpdateStatus(droneDto.Status ?? "Online");

            await _droneRepository.AddAsync(drone);

            return _mapper.Map<DroneDto>(drone);
        }

        public async Task<DroneDto> UpdateDroneAsync(DroneDto droneDto)
        {
            var existingDrone = await _droneRepository.GetByIdAsync(droneDto.Id);
            if (existingDrone == null)
            {
                throw new ApplicationException($"Drone com ID {droneDto.Id} não encontrado.");
            }

            _mapper.Map(droneDto, existingDrone); 

            await _droneRepository.UpdateAsync(existingDrone);
            
            return _mapper.Map<DroneDto>(existingDrone); 
        }

        public async Task DeleteDroneAsync(long id)
        {
            var drone = await _droneRepository.GetByIdAsync(id);
            if (drone == null)
            {
                throw new ApplicationException($"Drone com ID {id} não encontrado.");
            }
            await _droneRepository.DeleteAsync(drone);
        }
    }
}