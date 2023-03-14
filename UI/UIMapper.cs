using BLL.DTO;

using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.ViewModels;

namespace UI
{
    public static class UIMapper
    {
        public static CarDTO Map(this CarViewModel vm) =>
        new CarDTO
        {
            Name = vm.Name,
            Model = vm.Model,
            Number = vm.Number,
            YearProduct = vm.YearProduct,
            FuelConsumption = vm.FuelConsumption

        };
        public static CarViewModel Map(this CarDTO car) =>
        new CarViewModel
        {
            Id = car.Id,
            Name = car.Name,
            Model = car.Model,
            Number = car.Number,
            YearProduct = car.YearProduct,
            FuelConsumption = car.FuelConsumption
        };

        public static UserDTO Map(this UserViewModel vm)
        {

           return new UserDTO
            {
                FullName = vm.FullName,
                PhoneNumber = vm.PhoneNumber,
                PositionOffice = vm.PositionOffice,
                UserName = vm.UserName,
                Password = vm.Password,
                NameRole = vm.NameRole


           };
        }
        public static UserViewModel Map(this UserDTO user) =>
        new UserViewModel
        {
                Id = user.Id,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                PositionOffice = user.PositionOffice,
                UserName = user.UserName,
                Password = user.Password,
                NameRole=user.NameRole
        };

       public static DriverDTO Map(this DriverViewModel vm) =>
       new DriverDTO
       {
           FullName = vm.FullName,
           Adress = vm.Adress,
           PhoneNumber = vm.PhoneNumber,
           
       };
        public static DriverViewModel Map(this DriverDTO driver) =>
        new DriverViewModel
        {
            Id = driver.Id,
            FullName = driver.FullName,
            Adress = driver.Adress,
            PhoneNumber = driver.PhoneNumber,
        };
       // public static CarFuelConsumptionDTO Map(this DriverViewModel vm) =>
       //new DriverDTO
       //{
       //    FullName = vm.FullName,
       //    Adress = vm.Adress,
       //    PhoneNumber = vm.PhoneNumber,

       //};
       // public static DriverViewModel Map(this DriverDTO driver) =>
       // new DriverViewModel
       // {
       //     Id = driver.Id,
       //     FullName = driver.FullName,
       //     Adress = driver.Adress,
       //     PhoneNumber = driver.PhoneNumber,
       // };

    }


}