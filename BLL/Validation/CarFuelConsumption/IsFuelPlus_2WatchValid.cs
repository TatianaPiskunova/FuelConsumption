using BLL.Interfaces;
using DAL.Models;

namespace BLL.Specifications
{
    internal class IsFuelPlus_2WatchValid : ISpecification<CarFuelConsumption>
    {
        public bool IsSatisfiedBy(CarFuelConsumption entity)
        {
            return Convert.ToDouble(entity.FuelConsumptionPlus_2Watch) >= 0;
        }
    }
}
