using AutoMapper;
using SmartDrones.Application.DTOs;
using SmartDrones.Domain.Entities;

namespace SmartDrones.Application.Mappings
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Drone, DroneDto>().ReverseMap();

            CreateMap<SensorData, SensorDataDto>()
                .ReverseMap()
                .ForMember(dest => dest.Timestamp, opt => opt.Ignore());

            CreateMap<Alert, AlertDto>().ReverseMap();
        }
    }
}