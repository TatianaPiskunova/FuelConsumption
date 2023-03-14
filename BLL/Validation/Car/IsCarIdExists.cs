using BLL.Interfaces;
using DAL.Models;
using System.Linq;

namespace BLL.Specifications
{
    internal class IsCarIdExists : ISpecification<Car>
    {
        private readonly IQueryable<Car> _cars;

        public IsCarIdExists(IQueryable<Car> cars)
        {
            _cars = cars;
        }

        public bool IsSatisfiedBy(Car entity)
        {
            return _cars.Any(p => p.Id == entity.Id);
        }
    }
}
