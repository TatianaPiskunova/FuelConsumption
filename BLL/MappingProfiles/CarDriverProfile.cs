using AutoMapper;
using BLL.DTO;
using DAL.Models;

namespace BLL.MappingProfiles
{
    class CarDriverProfile : Profile
    {
        public CarDriverProfile()
        {
            CreateMap<CarDriver, CarDriverDTO>();
            CreateMap<CarDriverDTO, CarDriver>();
        }
    }
}
