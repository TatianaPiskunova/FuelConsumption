using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal class DriverRepository : IRepository<Driver>
    {
        public ApplicationContext Database { get; set; }

        public DriverRepository(ApplicationContext db)
        {
            Database = db;
        }
        public void Create(Driver item)
        {
            Database.Drivers.Add(item);
            Database.SaveChanges();
        }
        public void Delete(Driver item)
        {
            Database.Drivers.Remove(item);
            Database.SaveChanges();
        }
        public void Update(Driver item)
        {
            Database.Drivers.Update(item);
            Database.SaveChanges();
        }
        public IQueryable<Driver> GetAll() => Database.Drivers.AsQueryable().AsNoTracking();
    }
}
