using AutoMapper;
using BLL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.MappingProfiles
{
     class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<Driver, DriverDTO>().ReverseMap();
        }
    }
}
