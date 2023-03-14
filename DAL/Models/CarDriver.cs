using DAL.Interfaces;

namespace DAL.Models
{
    public class CarDriver : IModel
    {
        public int Id { get; set; }

        public int DriverId { get; set; }

        Driver Driver { get; set; }

        public int CarId { get; set; }

        Car Car { get; set; }

        public int WatchId { get; set; }
        Watch Watch { get; set; }
    }
}