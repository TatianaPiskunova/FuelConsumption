using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Specifications;
using DAL.Interfaces;

using DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    internal class DriverService : IDriverService
    {

        private readonly IRepository<Driver> _driverRepositry;
        private readonly IMapper _mapper;

        public DriverService(IRepository<Driver> driverRepositry, IMapper driverMapper)
        {
            _driverRepositry = driverRepositry;
            _mapper = driverMapper;
        }
        public void Add(DriverDTO item)
        {
            var driver = _mapper.Map<DriverDTO, Driver>(item);
            var drivers = _driverRepositry.GetAll();

            if (!new IsDriverFullNameValid().IsSatisfiedBy(driver))
            {
                throw new EntityArgumentException(
                    nameof(DriverDTO.FullName),
                    "ФИО должно состоять из более 3х символов");
            }
            if (new IsSameDriverFullNameExists(drivers).IsSatisfiedBy(driver))
            {
                throw new EntityArgumentException(
                    nameof(DriverDTO.FullName),
                    "Водитель с указанным именем уже существует");
            }
            if (!new IsDriverAdressValid().IsSatisfiedBy(driver))
            {
                throw new EntityArgumentException(
                    nameof(DriverDTO.Adress),
                    "Заполните адрес водителя");
            }
           

            _driverRepositry.Create(driver);
        }
        public DriverDTO FindByFullName(string fullName)
        {
            var driver = _driverRepositry.GetAll().FirstOrDefault(x => x.FullName == fullName);
            var driverDTO = _mapper.Map<Driver, DriverDTO>(driver);
            return driverDTO;
        }
        public void DeleteById(int id)
        {
            var driver = _driverRepositry.GetAll().FirstOrDefault(x => x.Id == id);

            if (driver != null)
            {
                _driverRepositry.Delete(driver);
            }
        }
        public void Update(DriverDTO item)
        {
            var driver = _mapper.Map<DriverDTO, Driver>(item);
            var drivers = _driverRepositry.GetAll();

            if (new IsSameDriverFullNameExists(drivers).IsSatisfiedBy(driver))
            {
                throw new EntityArgumentException(
                    nameof(DriverDTO.FullName),
                    "Водитель с указанным ФИО уже существует");
            }

            if (!new IsDriverFullNameValid().IsSatisfiedBy(driver))
            {
                throw new EntityArgumentException(
                    nameof(DriverDTO.FullName),
                    "Заполните ФИО водителя");
            }
            if (!new IsDriverAdressValid().IsSatisfiedBy(driver))
            {
                throw new EntityArgumentException(
                    nameof(DriverDTO.Adress),
                    "Заполните адрес водителя");
            }
            _driverRepositry.Update(driver);
        }

        DriverDTO IService<DriverDTO>.FindById(int id)
        {
            var driver = _driverRepositry.GetAll().FirstOrDefault(x => x.Id == id);
            var driverDTO = _mapper.Map<Driver, DriverDTO>(driver);
            return driverDTO;
        }

        List<DriverDTO> IService<DriverDTO>.GetAll()
        {
            return _mapper
                .Map<IQueryable<Driver>, List<DriverDTO>>(_driverRepositry.GetAll());
        }



    }
}