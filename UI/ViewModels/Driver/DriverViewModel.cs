using System.ComponentModel.DataAnnotations;
namespace UI.ViewModels
{
    public class DriverViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
