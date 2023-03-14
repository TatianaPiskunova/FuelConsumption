using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureDAL(this IServiceCollection services, string connection)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IRepository<Car>, CarRepository>();
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();
            services.AddScoped<IRepository<Driver>, DriverRepository>();
            services.AddScoped<IRepository<CarDriver>, CarDriverRepository>();
            services.AddScoped<IRepository<Watch>, WatchRepository>();
            services.AddScoped<IRepository<CarFuelConsumption>, CarFuelConsumptionRepository>();

        }
    }
}