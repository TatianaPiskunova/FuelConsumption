using BLL.Interfaces;
using DAL.Models;

namespace BLL.Specifications
{
    internal class IsCarNumberValid : ISpecification<Car>
    {
        public bool IsSatisfiedBy(Car entity)
        {
            return !string.IsNullOrEmpty(entity.Number) && entity.Number.Length >= 1;
        }
    }
}
