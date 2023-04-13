using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class UserDeleteViewModel
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string? UserName { get; set; }
    }
}
