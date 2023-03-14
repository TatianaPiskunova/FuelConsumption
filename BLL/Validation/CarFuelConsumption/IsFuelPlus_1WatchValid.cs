using BLL.Interfaces;
using DAL.Models;

namespace BLL.Specifications
{
    internal class IsFuelPlus_1WatchValid : ISpecification<CarFuelConsumption>
    {
        public bool IsSatisfiedBy(CarFuelConsumption entity)
        {
            return Convert.ToDouble(entity.FuelConsumptionPlus_1Watch) >= 0 ;
        }
    }
}
