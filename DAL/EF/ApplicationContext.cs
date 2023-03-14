using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DAL
{
    internal class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext()
        {

        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Car;Integrated Security=True");
        }

        public new DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<CarDriver> CarDrivers { get; set; }
        public DbSet<Watch> Watches { get; set; }
        public DbSet<CarFuelConsumption> CarFuelConsumptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var watch1 = new Watch()
            {
                Id = 1,
                NumberWatch = "1",
            };

            var watch2 = new Watch()
            {
                Id = 2,
                NumberWatch = "2",
            };
            modelBuilder.Entity<Watch>().HasData(new Watch[] { watch1, watch2 });

            var fuelConsumption1 = new CarFuelConsumption()
            {
                Id=1,
                CarId = 1,
                WatchNumberId = 1,
                DriverId = 1,
                Mounth="январь",
                Year = "2022",
                EqualFuelConsumptionFirst_1Watch ="100",
                FuelConsumptionPlus_1Watch= "100",
                FuelConsumptionMinus_1Watch= "150",
                EqualFuelConsumptionLast_1Watch= "50",
                EqualFuelConsumptionFirst_2Watch=null,
                FuelConsumptionPlus_2Watch=null,
                FuelConsumptionMinus_2Watch=null,
                EqualFuelConsumptionLast_2Watch=null,

            };
            var fuelConsumption2 = new CarFuelConsumption()
            {
                Id = 2,
                CarId = 1,
                WatchNumberId = 2,
                DriverId = 3,
                Mounth = "январь",
                Year = "2022",
                EqualFuelConsumptionFirst_1Watch = null,
                FuelConsumptionPlus_1Watch = null,
                FuelConsumptionMinus_1Watch = null,
                EqualFuelConsumptionLast_1Watch = null,
                EqualFuelConsumptionFirst_2Watch = "50",
                FuelConsumptionPlus_2Watch = "200",
                FuelConsumptionMinus_2Watch = "80",
                EqualFuelConsumptionLast_2Watch = "170",

            };
            modelBuilder.Entity<CarFuelConsumption>().HasData(new CarFuelConsumption[] { fuelConsumption1, fuelConsumption2 });


            var driver1 = new Driver()
            {
                Id = 1,
                FullName = "Карп Степан Степанович",
                Adress = "г.Гомель, Пушкина 3/15",
                PhoneNumber = "111111111",
            };

            var driver2 = new Driver()
            {
                Id = 2,
                FullName = "Жарков Юрий Степанович",
                Adress = "г.Гомель, пр.Ленина 3/15",
                PhoneNumber = "111223111",
            };
            var driver3 = new Driver()
            {
                Id = 3,
                FullName = "Чуйко Федор Михайлович",
                Adress = "г.Гомель, Книжная 16/4",
                PhoneNumber = "222221111",
            };
            var driver4 = new Driver()
            {
                Id = 4,
                FullName = "Асиапов Иван Юрьевич",
                Adress = "г.Речица, Пушкина 3/15",
                PhoneNumber = "111111111",
            };
            var driver5 = new Driver()
            {
                Id = 5,
                FullName = "Юрик Павел Олегович",
                Adress = "г.Гомель, Сосновая 1",
                PhoneNumber = "33333333",
            };
            var driver6 = new Driver()
            {
                Id = 6,
                FullName = "Ростов Никита Кириллович",
                Adress = "г.Гомель, Советская 103/15",
                PhoneNumber = "666666666",
            };
            var driver7 = new Driver()
            {
                Id = 7,
                FullName = "Ростов Руслан Кириллович",
                Adress = "г.Гомель, Советская 103/15",
                PhoneNumber = "545454545",
            };

            modelBuilder.Entity<Driver>().HasData(new Driver[] { driver1, driver2, driver3, driver4, driver5, driver6, driver7 });
            var car1 = new Car()
            {
                Id = 1,
                Name = "МАЗ",
                Model = "5516",
                Number = "1234AA-3",
                YearProduct = 2008,
                FuelConsumption = 45.50,

            };
            var listDriver1 = new List<CarDriver>
            {
                new CarDriver { Id = 1, DriverId = driver1.Id, CarId=car1.Id, WatchId=watch1.Id },
                new CarDriver { Id = 2, DriverId = driver3.Id, CarId=car1.Id, WatchId=watch2.Id },

            };
            var car2 = new Car()
            {
                Id = 2,
                Name = "МАЗ",
                Model = "5516",
                Number = "2345AA-3",
                YearProduct = 2008,
                FuelConsumption = 42.50,

            };
            var listDriver2 = new List<CarDriver>
            {
                new CarDriver { Id = 3, DriverId = driver2.Id, CarId=car2.Id,  WatchId=watch1.Id  },
                new CarDriver { Id = 4, DriverId = driver4.Id, CarId=car2.Id,  WatchId=watch2.Id },

            };
            var car3 = new Car()
            {
                Id = 3,
                Name = "МАЗ",
                Model = "5516",
                Number = "3456AA-3",
                YearProduct = 2010,
                FuelConsumption = 41.00,

            };
            var listDriver3 = new List<CarDriver>
            {
                new CarDriver { Id = 5, DriverId = driver5.Id, CarId=car3.Id,  WatchId=watch1.Id},
                new CarDriver { Id = 6, DriverId = driver7.Id, CarId = car3.Id, WatchId=watch2.Id},

            };

            var drivers = new List<CarDriver>(listDriver1);
            drivers.AddRange(listDriver2);
            drivers.AddRange(listDriver3);

            modelBuilder.Entity<CarDriver>().HasData(drivers);
            modelBuilder.Entity<Car>().HasData(new Car[] { car1, car2, car3 });





        }
    }
}
