using BLL.Interfaces;

namespace BLL.DTO
{
    public class CarDTO : IDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Model { get; set; }
        public string? Number { get; set; }
        public int YearProduct { get; set; }
        public double FuelConsumption { get; set; }
    }
}
