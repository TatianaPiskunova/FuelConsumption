using DAL.Interfaces;
using System.Globalization;

namespace DAL.Models
{
    public class CarFuelConsumption : IModel
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

        //public string EqualFuelConsumptionLast_1Watch="";
        //public string EqualFuelConsumptionLast1Watch 
        //{
        //    set
        //    {
        //        EqualFuelConsumptionLast_1Watch = (double.Parse(EqualFuelConsumptionFirst_1Watch) + double.Parse(FuelConsumptionPlus_1Watch) - double.Parse(FuelConsumptionMinus_1Watch)).ToString("F", CultureInfo.CreateSpecificCulture("fr-FR"));
        //    }
        //    get {
        //        return EqualFuelConsumptionLast_1Watch;
        //    }
            
        //}
        public string? EqualFuelConsumptionFirst_2Watch { get; set; }
        public string? FuelConsumptionPlus_2Watch { get; set; }
        public string? FuelConsumptionMinus_2Watch { get; set; }
        public string? EqualFuelConsumptionLast_2Watch { get; set; }



        //public string EqualFuelConsumptionLast_2Watch="";
        //public string EqualFuelConsumptionLast2Watch
        //{
        //    set
        //    {
        //        EqualFuelConsumptionLast_2Watch = (double.Parse(EqualFuelConsumptionFirst_2Watch) + double.Parse(FuelConsumptionPlus_2Watch) - double.Parse(FuelConsumptionMinus_2Watch)).ToString("F", CultureInfo.CreateSpecificCulture("fr-FR"));
        //    }
        //    get
        //    {
        //        return EqualFuelConsumptionLast_2Watch;
        //    }

        //}
    }
}