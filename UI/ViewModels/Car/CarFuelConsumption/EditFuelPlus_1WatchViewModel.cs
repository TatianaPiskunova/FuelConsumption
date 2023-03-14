using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class EditFuelPlus_1WatchViewModel: IdForUpdateViewModel
    {

        [Required]
        public string? FuelConsumptionPlus_1Watch { get; set; }
    }
}
