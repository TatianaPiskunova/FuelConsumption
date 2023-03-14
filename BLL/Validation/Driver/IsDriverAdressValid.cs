using BLL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Specifications
{
    internal class IsDriverAdressValid : ISpecification<Driver>
    {
        public bool IsSatisfiedBy(Driver entity)
        {
            return !string.IsNullOrEmpty(entity.Adress) && entity.Adress.Length >= 1;
        }
    }
}
