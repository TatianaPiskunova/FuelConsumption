using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{ 
    public class AddFuelConsumptionViewModel
    {
        [Required]
        public string? Number { get; set; }
        [Required]
        public string? FuelConsumptionPlus { get; set; }
        [Required]
        public string? FuelConsumptionMinus { get; set; }
        [Required]
        public string? Mounth { get; set; }
        [Required]
        public string? Year { get; set; }
        [Required]
        public string? WatchNumber { get; set; }

    }
}
