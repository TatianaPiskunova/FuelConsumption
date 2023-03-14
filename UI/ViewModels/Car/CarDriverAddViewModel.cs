using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class CarDriverAddViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Watch { get; set; }
    }
}