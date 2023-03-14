using BLL.Interfaces;
using DAL.Models;

namespace BLL.Specifications
{
    internal class IsSameCarNumberExists : ISpecification<Car>
    {
        private readonly IQueryable<Car> _cars;

        public IsSameCarNumberExists(IQueryable<Car> cars)
        {
            _cars = cars;
        }

        public bool IsSatisfiedBy(Car entity)
        {
            return _cars.Any(p => p.Id != entity.Id && p.Number == entity.Number);
        }
    }
}