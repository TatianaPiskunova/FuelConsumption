using BLL.Interfaces;
using DAL.Models;

namespace BLL.Specifications
{
    internal class IsCarNameValid : ISpecification<Car>
    {
        public bool IsSatisfiedBy(Car entity)
        {
            return !string.IsNullOrEmpty(entity.Name) && entity.Name.Length >= 1;
        }
    }
}
