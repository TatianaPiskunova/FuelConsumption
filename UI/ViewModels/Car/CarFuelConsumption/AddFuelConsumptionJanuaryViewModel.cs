using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class AddFuelConsumptionJanuaryViewModel: AddFuelConsumptionViewModel
    {
        [Required]
        public string? FuelConsumptionDecember { get; set; }
      

    }
}
