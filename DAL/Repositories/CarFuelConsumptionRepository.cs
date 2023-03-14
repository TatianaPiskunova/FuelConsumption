using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    internal class CarFuelConsumptionRepository : IRepository<CarFuelConsumption>
    {
        public ApplicationContext Database { get; set; }

        public CarFuelConsumptionRepository(ApplicationContext db)
        {
            Database = db;
        }

        public void Create(CarFuelConsumption item)
        {
            Database.CarFuelConsumptions.Add(item);
            Database.SaveChanges();
        }

        public void Delete(CarFuelConsumption item)
        {
            Database.CarFuelConsumptions.Remove(item);
            Database.SaveChanges();
        }

        public void Update(CarFuelConsumption item)
        {
            Database.CarFuelConsumptions.Update(item);
            Database.SaveChanges();
        }

        public IQueryable<CarFuelConsumption> GetAll() => Database.CarFuelConsumptions.AsQueryable().AsNoTracking();
    }
}