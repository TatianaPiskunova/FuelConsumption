using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class CarDeleteViewModel
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string Number { get; set; }
    }
}
