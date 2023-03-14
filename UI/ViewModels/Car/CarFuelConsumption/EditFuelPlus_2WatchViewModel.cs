using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class EditFuelPlus_2WatchViewModel: IdForUpdateViewModel
    {

        [Required]
        public string? FuelConsumptionPlus_2Watch { get; set; }
    }
}
