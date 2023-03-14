using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class SearchCarViewModel
    {
        [Required]
        public string Number { get; set; }
    }
}