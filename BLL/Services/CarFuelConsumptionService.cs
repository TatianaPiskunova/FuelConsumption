using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Specifications;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class CarFuelConsumptionService:ICarFuelConsumptionService
    {
              
        private readonly IMapper _mapper;
        private readonly IRepository<CarFuelConsumption> _carFuelConsumptionRepositry;

        public CarFuelConsumptionService(IMapper carMapper,
            IRepository<CarFuelConsumption> carFuelConsumptionRepositry)
        {
          
            _mapper = carMapper;
            _carFuelConsumptionRepositry = carFuelConsumptionRepositry;
        }

        public List<CarFuelConsumptionDTO> GetAll()
        {
            return _mapper
                .Map<IQueryable<CarFuelConsumption>, List<CarFuelConsumptionDTO>>(_carFuelConsumptionRepositry.GetAll());

        }
        public void Add(CarFuelConsumptionDTO item)
        {
            var carFuelConsumption = _mapper.Map<CarFuelConsumptionDTO, CarFuelConsumption>(item);
            var carFuelConsumptions = _carFuelConsumptionRepositry.GetAll();

            if (!new IsFuelPlus_1WatchValid().IsSatisfiedBy(carFuelConsumption))
            {
                throw new EntityArgumentException(
                    nameof(CarFuelConsumptionDTO.FuelConsumptionPlus_1Watch),
                    "Введите правильно число!!! Разделитель - '.' (точка). Например 23.3");
            }
            if (!new IsFuelPlus_2WatchValid().IsSatisfiedBy(carFuelConsumption))
            {
                throw new EntityArgumentException(
                    nameof(CarFuelConsumptionDTO.FuelConsumptionPlus_2Watch),
                    "Введите правильно число!!! Разделитель -  ',' (запятая). Например 23,3");
            }
            if (!new IsFuelMinus_1WatchValid().IsSatisfiedBy(carFuelConsumption))
            {
                throw new EntityArgumentException(
                    nameof(CarFuelConsumptionDTO.FuelConsumptionMinus_1Watch),
                    "Введите правильно число!!! Разделитель - ',' (запятая). Например 23,3");
            }
            if (!new IsFuelMinus_2WatchValid().IsSatisfiedBy(carFuelConsumption))
            {
                throw new EntityArgumentException(
                    nameof(CarFuelConsumptionDTO.FuelConsumptionMinus_2Watch),
                    "Введите правильно число!!! Разделитель -  ',' (запятая). Например 23,3");
            }

            _carFuelConsumptionRepositry.Create(carFuelConsumption);
        }
        public CarFuelConsumptionDTO FindById(int id)
        {
            var carFuelConsumption = _carFuelConsumptionRepositry.GetAll().FirstOrDefault(x => x.Id == id);
            var carFuelConsumptionDTO = _mapper.Map<CarFuelConsumption, CarFuelConsumptionDTO>(carFuelConsumption);
            return carFuelConsumptionDTO;
        }

        public void DeleteById(int id)
        {
            var carFuelConsumption = _carFuelConsumptionRepositry.GetAll().FirstOrDefault(x => x.Id == id);

            if (carFuelConsumption != null)
            {
                _carFuelConsumptionRepositry.Delete(carFuelConsumption);
            }
        }
        public void Update(CarFuelConsumptionDTO item)
        {

            var carFuelConsumption = _mapper.Map<CarFuelConsumptionDTO, CarFuelConsumption>(item);
            var carFuelConsumptions = _carFuelConsumptionRepositry.GetAll();

            _carFuelConsumptionRepositry.Update(carFuelConsumption);
        }
        public string NameMounth(string mounth)
        {
            switch (
                (mounth.ToLower() == "январь") ? 1
                : (mounth.ToLower() == "февраль") ? 2
                : (mounth.ToLower() == "март") ? 3
                : (mounth.ToLower() == "апрель") ? 4
                : (mounth.ToLower() == "май") ? 5
                : (mounth.ToLower() == "июнь") ? 6
                : (mounth.ToLower() == "июль") ? 7
                : (mounth.ToLower() == "август") ? 8
                : (mounth.ToLower() == "сентябрь") ? 9
                : (mounth.ToLower() == "октябрь") ? 10
                : (mounth.ToLower() == "ноябрь") ? 11
                : (mounth.ToLower() == "декабрь") ? 12 : default)
            {
                case 1: return "january";
                case 2: return "январь";
                case 3: return "февраль";
                case 4: return "март";
                case 5: return "апрель";
                case 6: return "май";
                case 7: return "июнь";
                case 8: return "июль";
                case 9: return "август";
                case 10: return "сентябрь";
                case 11: return "октябрь"; ;
                case 12: return "ноябрь";
                default: return "error";
                  
            }
        }

    }
}

