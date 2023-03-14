using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Specifications;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BLL.Services
{
 
    internal class CarService : ICarService
    {
      
        private readonly IRepository<Car> _carRepositry;
        private readonly IMapper _mapper;
        private readonly IRepository<CarDriver> _carDriverRepositry;
        private readonly IRepository<Driver> _driverRepositry;
        private readonly IRepository<Watch> _watchRepositry;
        private readonly IRepository<CarFuelConsumption> _carFuelConsumptionRepositry;

        public CarService(IRepository<Car> carRepositry, IMapper carMapper, 
            IRepository<CarDriver> carDriverRepositry,
            IRepository<Driver> driverRepositry,
            IRepository<Watch> watchRepositry,
            IRepository<CarFuelConsumption> carFuelConsumptionRepositry)
        {
            _carRepositry = carRepositry;
            _mapper = carMapper;
            _carDriverRepositry = carDriverRepositry;
            _driverRepositry = driverRepositry;
            _watchRepositry = watchRepositry;
            _carFuelConsumptionRepositry = carFuelConsumptionRepositry;
        }


        public void Add(CarDTO item)
        {
            var car = _mapper.Map<CarDTO, Car>(item);
            var cars = _carRepositry.GetAll();

            if (!new IsCarNameValid().IsSatisfiedBy(car))
            {
                throw new EntityArgumentException(
                    nameof(CarDTO.Name),
                    "Заполните марку автомобиля");
            }
            if (!new IsCarModelValid().IsSatisfiedBy(car))
            {
                throw new EntityArgumentException(
                    nameof(CarDTO.Model),
                    "Заполните модель автомобиля");
            }
            if (!new IsCarNumberValid().IsSatisfiedBy(car))
            {
                throw new EntityArgumentException(
                    nameof(CarDTO.Number),
                    "Заполните номер автомобиля");
            }

            if (new IsSameCarNumberExists(cars).IsSatisfiedBy(car))
            {
                throw new EntityArgumentException(
                    nameof(CarDTO.Number),
                    "Автомобиль с указанным номером уже существует");
            }

            if (!new IsCarYearProductValid().IsSatisfiedBy(car))
            {
                throw new EntityArgumentException(
                    nameof(CarDTO.YearProduct),
                    "Проверьте указанный год выпуска");
            }

            if (!new IsCarFuelConsumptionValid().IsSatisfiedBy(car))
            {
                throw new EntityArgumentException(
                    nameof(CarDTO.FuelConsumption),
                    "Проверьте линейный расход топлива");
            }

            _carRepositry.Create(car);
        }

        public CarDTO FindByNumber(string number)
        {
            var car = _carRepositry.GetAll().FirstOrDefault(x => x.Number == number);
            var carDTO = _mapper.Map<Car, CarDTO>(car);
            return carDTO;
        }
        public void DeleteById(int id)
        {
            var car = _carRepositry.GetAll().FirstOrDefault(x => x.Id == id);

            if (car != null)
            {
                _carRepositry.Delete(car);
            }
        }

        public void Update(CarDTO item)
        {
            var car = _mapper.Map<CarDTO, Car>(item);
            var cars = _carRepositry.GetAll();


            if (!new IsCarIdExists(cars).IsSatisfiedBy(car))
            {
                throw new InvalidIdException("Автомобиль с указанным порядковым номером не найден");
            }

            if (new IsSameCarNumberExists(cars).IsSatisfiedBy(car))
            {
                throw new EntityArgumentException(
                    nameof(CarDTO.Number),
                    "Автомобиль с указанным номером уже существует");
            }

            if (!new IsCarNameValid().IsSatisfiedBy(car))
            {
                throw new EntityArgumentException(
                    nameof(CarDTO.Name),
                    "Заполните марку автомобиля");
            }
            if (!new IsCarModelValid().IsSatisfiedBy(car))
            {
                throw new EntityArgumentException(
                    nameof(CarDTO.Model),
                    "Заполните модель автомобиля");
            }
            if (!new IsCarNumberValid().IsSatisfiedBy(car))
            {
                throw new EntityArgumentException(
                    nameof(CarDTO.Number),
                    "Заполните номер автомобиля");
            }

            if (!new IsCarYearProductValid().IsSatisfiedBy(car))
            {
                throw new EntityArgumentException(
                    nameof(CarDTO.YearProduct),
                    "Проверьте указанный год выпуска");
            }

            if (!new IsCarFuelConsumptionValid().IsSatisfiedBy(car))
            {
                throw new EntityArgumentException(
                    nameof(CarDTO.FuelConsumption),
                    "Проверьте линейный расход топлива");
            }

            _carRepositry.Update(car);
        }
        public List<DriverDTO> GetDrivers(int id)
        {
            
            var drivers = _driverRepositry.GetAll().ToList()
               .Where(i => _carDriverRepositry.GetAll().ToList()
                                                  .Where(pi => pi.CarId == id)
                                                  .Select(pi => pi.DriverId)
                                                  .ToList()
                                                  .Contains(i.Id))
               .ToList();
            List<DriverDTO> driversDTO =
                drivers.Select(i => (_mapper.Map<Driver, DriverDTO>(i))).ToList();

            return driversDTO;

        }


        public List<WatchDTO> GetNumberWatchForDriver(int driverId)
        {
            var watches = _watchRepositry.GetAll().ToList()
              .Where(i => _carDriverRepositry.GetAll().ToList()
                                                 .Where(pi => pi.DriverId == driverId)
                                                 .Select(pi => pi.WatchId)
                                                 .ToList()
                                                 .Contains(i.Id))
              .ToList();
            List<WatchDTO> watchesDTO =
                watches.Select(i => (_mapper.Map<Watch, WatchDTO>(i))).ToList();

            return watchesDTO;

        }
        public List<DriverDTO> GetDriverForNumberWatch(int watchId, int carId)
        {
            var drivers = _driverRepositry.GetAll().ToList()
              .Where(i => _carDriverRepositry.GetAll().ToList()
                                                 .Where(pi => pi.WatchId == watchId)
                                                 .Where(pi => pi.CarId == carId)
                                                 .Select(pi => pi.DriverId)
                                                 .ToList()
                                                 .Contains(i.Id))
              .ToList();
            List<DriverDTO> driversDTO =
                drivers.Select(i => (_mapper.Map<Driver, DriverDTO>(i))).ToList();

            return driversDTO;

        }

        public void AddCarDriver(int idCar, int idDriver, int idNumberWatch)
        {
            var carDriver = _carDriverRepositry.GetAll().FirstOrDefault(x => x.DriverId == idDriver);

            if (carDriver == null)
            {
                var cd = new CarDriver
                {
                    CarId = idCar,
                    DriverId = idDriver,
                    WatchId = idNumberWatch
                };

                _carDriverRepositry.Create(cd);
            }
            else throw new Exception ("Водитель на автомобиле присутствует");
                     
        }
        CarDTO IService<CarDTO>.FindById(int id)
        {
            var car = _carRepositry.GetAll().FirstOrDefault(x => x.Id == id);
            var carDTO = _mapper.Map<Car, CarDTO>(car);
            return carDTO;
        }

        List<CarDTO> IService<CarDTO>.GetAll()
        {
            return _mapper
                .Map<IQueryable<Car>, List<CarDTO>>(_carRepositry.GetAll());
        }


        public WatchDTO FindByNumberWatch(string numberWatch)
        {
            var watch = _watchRepositry.GetAll().FirstOrDefault(x => x.NumberWatch == numberWatch);
            var watchDTO = _mapper.Map<Watch, WatchDTO>(watch);
            return watchDTO;
        }

        public WatchDTO FindByNumberWatchId(int numberWatchId)
        {
            var watch = _watchRepositry.GetAll().FirstOrDefault(x => x.Id == numberWatchId);
            var watchDTO = _mapper.Map<Watch, WatchDTO>(watch);
            return watchDTO;
        }

       
    }
}
