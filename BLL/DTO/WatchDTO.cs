using BLL.Interfaces;

namespace BLL.DTO
{
    public class WatchDTO : IDTO
    {
        public int Id { get; set; }
        public string? NumberWatch { get; set; }
    }
}
