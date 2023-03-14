using BLL.Interfaces;
using DAL.Models;

namespace BLL.Specifications
{
    internal class IsCarFuelConsumptionValid : ISpecification<Car>
    {
        public bool IsSatisfiedBy(Car entity)
        {
            return entity.FuelConsumption > 0 && entity.FuelConsumption < 100;
        }
    }
}