using BLL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICarFuelConsumptionService
    {
        List<CarFuelConsumptionDTO> GetAll();
        void Add(CarFuelConsumptionDTO item);
        CarFuelConsumptionDTO FindById(int id);
        void DeleteById(int id);
        void Update(CarFuelConsumptionDTO item);
        string NameMounth(string mounth);
    }
}
