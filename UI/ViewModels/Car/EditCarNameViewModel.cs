using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class EditCarNameViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
