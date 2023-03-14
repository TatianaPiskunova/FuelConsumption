using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class UserViewModel
    {
        public string? Id { get; set; }
        
        [Required]
        public string FullName { get; set; }

        [Required]
        public string PositionOffice { get; set; }

        //[Required]
        //[DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserName { get; set; } //логин

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required]
        public string NameRole { get; set; }
    }
}
