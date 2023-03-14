using DAL.Interfaces;

namespace DAL.Models
{
    public class Watch : IModel
    {
        public int Id { get; set; }
        public string? NumberWatch { get; set; }
       
    }
}
