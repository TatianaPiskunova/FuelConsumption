using BLL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Specifications
{
    internal class IsDriverFullNameValid : ISpecification<Driver>
    {
        public bool IsSatisfiedBy(Driver entity)
        {
            var fullName = entity.FullName;
            return !string.IsNullOrEmpty(fullName) && fullName.Length >= 3;
        }
    }
}