using BLL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Specifications
{
    internal class IsSameUserFullNameExists : ISpecification<User>
    {
        private readonly IQueryable<User> _users;

        public IsSameUserFullNameExists(IQueryable<User> users)
        {
            _users = users;
        }

        public bool IsSatisfiedBy(User entity)
        {
            return _users.Any(u => u.Id != entity.Id && u.FullName == entity.FullName);
        }
    }
}
