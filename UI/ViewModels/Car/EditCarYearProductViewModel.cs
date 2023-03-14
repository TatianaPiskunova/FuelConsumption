using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class EditCarYearProductViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int YearProduct { get; set; }
    }
}
