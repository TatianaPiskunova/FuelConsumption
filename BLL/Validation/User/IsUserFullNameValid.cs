using BLL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Specifications
{
    internal class IsUserFullNameValid : ISpecification<User>
    {
        public bool IsSatisfiedBy(User entity)
        {
            var fullName = entity.FullName;
            return !string.IsNullOrEmpty(fullName) && fullName.Length >= 3;
        }
    }
}
