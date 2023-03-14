using DAL.Interfaces;

namespace DAL.Models
{
    public class Car:IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Number { get; set; }
        public int YearProduct { get; set; }
        public double FuelConsumption { get; set; }
        public List<CarDriver> Drivers { get; set; }
    }
}
