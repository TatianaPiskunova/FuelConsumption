using BLL.Interfaces;
using DAL.Models;

namespace BLL.Specifications
{
    internal class IsCarYearProductValid : ISpecification<Car>
    {
        public bool IsSatisfiedBy(Car entity)
        {
            return entity.YearProduct > 1900 && entity.YearProduct < 2023;
        }
    }
}

