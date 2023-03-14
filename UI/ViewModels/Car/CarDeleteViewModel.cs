using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class CarDeleteViewModel
    {
        [Required]
        public string Number { get; set; }
    }
}
