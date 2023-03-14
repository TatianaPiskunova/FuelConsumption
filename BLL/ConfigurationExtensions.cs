using Microsoft.Extensions.DependencyInjection;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.MappingProfiles;
using BLL.Specifications;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;

namespace BLL
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureBLL(this IServiceCollection services, string connection)
        {
            services.ConfigureDAL(connection);
            services.AddAutoMapper(
                typeof(CarProfile),
                typeof(UserProfile),
                typeof(DriverProfile),
                typeof(CarDriverProfile), 
                typeof(WatchProfile),
                typeof(CarFuelConsumptionProfile)
   
                );
            services.AddTransient<ICarService, CarService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IDriverService, DriverService>();
            services.AddTransient<ICarFuelConsumptionService, CarFuelConsumptionService>();
            services.AddTransient<IWatchService, WatchService>();

        }
    }
}