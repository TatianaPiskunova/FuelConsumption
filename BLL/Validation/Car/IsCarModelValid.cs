using BLL.Interfaces;
using DAL.Models;

namespace BLL.Specifications
{
    internal class IsCarModelValid : ISpecification<Car>
    {
        public bool IsSatisfiedBy(Car entity)
        {
            return !string.IsNullOrEmpty(entity.Model) && entity.Model.Length >= 1;
        }
    }
}
