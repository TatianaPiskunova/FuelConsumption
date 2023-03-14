using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    internal class CarDriverRepository : IRepository<CarDriver>
    {
        public ApplicationContext Database { get; set; }

        public CarDriverRepository(ApplicationContext db)
        {
            Database = db;
        }

        public void Create(CarDriver item)
        {
            Database.CarDrivers.Add(item);
            Database.SaveChanges();
        }

        public void Delete(CarDriver item)
        {
            Database.CarDrivers.Remove(item);
            Database.SaveChanges();
        }

        public void Update(CarDriver item)
        {
            Database.CarDrivers.Update(item);
            Database.SaveChanges();
        }

        public IQueryable<CarDriver> GetAll() => Database.CarDrivers.AsQueryable().AsNoTracking();
    }
}