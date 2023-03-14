using BLL.Interfaces;
using DAL.Models;
using System.Linq;

namespace BLL.Specifications
{
    internal class IsSameUserNameExists : ISpecification<User>
    {
        private readonly IQueryable<User> _users;

        public IsSameUserNameExists(IQueryable<User> users)
        {
            _users = users;
        }

        public bool IsSatisfiedBy(User entity)
        {
            return _users.Any(u => u.Id != entity.Id && u.UserName == entity.UserName);
        }
    }
}
