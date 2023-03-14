using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class DriverDeleteViewModel
    {
        [Required]
        public string FullNumber { get; set; }
    }
}
