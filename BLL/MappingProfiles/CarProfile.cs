using AutoMapper;
using BLL.DTO;
using DAL.Models;

namespace BLL.MappingProfiles
{
    class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDTO>().ReverseMap();
        }
    }
}