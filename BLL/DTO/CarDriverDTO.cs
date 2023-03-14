using BLL.Interfaces;

namespace BLL.DTO
{
    public class CarDriverDTO : IDTO
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public int CarId { get; set; }
        public int WatchId { get; set; }

    }
}