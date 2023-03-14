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
    internal class WatchRepository : IRepository<Watch>
    {
        public ApplicationContext Database { get; set; }

        public WatchRepository(ApplicationContext db)
        {
            Database = db;
        }
        public void Create(Watch item)
        {
            Database.Watches.Add(item);
            Database.SaveChanges();
        }
        public void Delete(Watch item)
        {
            Database.Watches.Remove(item);
            Database.SaveChanges();
        }
        public void Update(Watch item)
        {
            Database.Watches.Update(item);
            Database.SaveChanges();
        }
        public IQueryable<Watch> GetAll() => Database.Watches.AsQueryable().AsNoTracking();
    }
}
