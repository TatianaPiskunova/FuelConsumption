using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class EditCarNumberViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }
    }
}
