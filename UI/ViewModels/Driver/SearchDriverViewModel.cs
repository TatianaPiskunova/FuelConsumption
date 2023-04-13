using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class SearchDriverViewModel
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string FullName { get; set; }
    }
}