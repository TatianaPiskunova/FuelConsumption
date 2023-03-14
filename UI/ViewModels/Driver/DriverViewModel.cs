using System.ComponentModel.DataAnnotations;
namespace UI.ViewModels
{
    public class DriverViewModel
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Adress { get; set; }
               
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
