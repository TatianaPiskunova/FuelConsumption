using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class EditCarModelViewModel
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }
    }
}
