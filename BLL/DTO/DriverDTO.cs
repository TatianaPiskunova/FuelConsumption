using BLL.Interfaces;

namespace BLL.DTO
{
    public class DriverDTO : IDTO
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Adress { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
