using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    internal class CarRepository : IRepository<Car>
    {
        public ApplicationContext Database { get; set; }

        public CarRepository(ApplicationContext db)
        {
            Database = db;
        }
        public void Create(Car item)
        {
            Database.Cars.Add(item);
            Database.SaveChanges();
        }
        public void Delete(Car item)
        {
            Database.Cars.Remove(item);
            Database.SaveChanges();
        }
        public void Update(Car item)
        {
            Database.Cars.Update(item);
            Database.SaveChanges();
        }
        public IQueryable<Car> GetAll() => Database.Cars.AsQueryable().AsNoTracking();
    }
}
