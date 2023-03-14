using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class EditDriverPhoneNumberViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
