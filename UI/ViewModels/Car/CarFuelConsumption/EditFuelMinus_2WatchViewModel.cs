using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class EditFuelMinus_2WatchViewModel: IdForUpdateViewModel
    {

        [Required]
        public string? FuelConsumptionMinus_2Watch { get; set; }
    }
}
