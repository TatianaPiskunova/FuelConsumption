using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{ 
    public class EditFuelMinus_1WatchViewModel: IdForUpdateViewModel
    {
        [Required]
        public string? FuelConsumptionMinus_1Watch { get; set; }
    }
}
