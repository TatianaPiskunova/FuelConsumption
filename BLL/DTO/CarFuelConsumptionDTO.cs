using BLL.Interfaces;
using System.Globalization;

namespace BLL.DTO
{
   public class CarFuelConsumptionDTO:IDTO
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int WatchNumberId { get; set; }
        public int DriverId { get; set; }
        public string? Mounth { get; set; }
        public string? Year { get; set; }
        public string? EqualFuelConsumptionFirst_1Watch { get; set; }
        public string? FuelConsumptionPlus_1Watch { get; set; }
        public string? FuelConsumptionMinus_1Watch { get; set; }
        public string? EqualFuelConsumptionLast_1Watch { get; set; }
        public string? EqualFuelConsumptionFirst_2Watch { get; set; }
        public string? FuelConsumptionPlus_2Watch { get; set; }
        public string? FuelConsumptionMinus_2Watch { get; set; }
         public string? EqualFuelConsumptionLast_2Watch { get; set; }


    }
}
