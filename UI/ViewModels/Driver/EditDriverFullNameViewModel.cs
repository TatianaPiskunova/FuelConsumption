using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class EditDriverFullNameViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }
    }
}
