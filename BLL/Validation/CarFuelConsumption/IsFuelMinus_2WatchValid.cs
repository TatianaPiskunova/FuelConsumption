using BLL.Interfaces;
using DAL.Models;

namespace BLL.Specifications
{
    internal class IsFuelMinus_2WatchValid : ISpecification<CarFuelConsumption>
    {
        public bool IsSatisfiedBy(CarFuelConsumption entity)
        {
            return Convert.ToDouble(entity.FuelConsumptionMinus_2Watch) >= 0;
        }
    }
}
