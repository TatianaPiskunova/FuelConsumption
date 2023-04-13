using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class UserViewModel
    {
        public string? Id { get; set; }
        
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string PositionOffice { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string UserName { get; set; } //логин

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required]
        public string NameRole { get; set; }
    }
}
