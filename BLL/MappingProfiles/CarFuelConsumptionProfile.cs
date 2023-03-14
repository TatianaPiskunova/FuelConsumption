using AutoMapper;
using BLL.DTO;
using DAL.Models;

namespace BLL.MappingProfiles
{
    class CarFuelConsumptionProfile : Profile
    {
        public CarFuelConsumptionProfile()
        {
            CreateMap<CarFuelConsumption, CarFuelConsumptionDTO>();
            CreateMap<CarFuelConsumptionDTO, CarFuelConsumption>();
        }
    }
}
