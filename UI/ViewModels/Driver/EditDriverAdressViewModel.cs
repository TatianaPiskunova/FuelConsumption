using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class EditDriverAdressViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Adress { get; set; }
    }
}
