using BLL.Interfaces;

using DAL.Models;

namespace BLL.Specifications
{
    internal class IsSameDriverFullNameExists : ISpecification<Driver>
    {
        private readonly IQueryable<Driver> _drivers;

        public IsSameDriverFullNameExists(IQueryable<Driver> drivers)
        {
            _drivers = drivers;
        }

        public bool IsSatisfiedBy(Driver entity)
        {
            return _drivers.Any(p => p.Id != entity.Id && p.FullName == entity.FullName);
        }
    }
}