using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class ShowCarDriverViewModel
    {
        [Required]
        public string Number { get; set; }
    }
}