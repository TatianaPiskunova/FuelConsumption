using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class DeleteFuelConsumptionViewModel
    {
        [Required]
        public string? Number { get; set; }
        [Required]
        public string? Mounth { get; set; }
        [Required]
        public string? Year { get; set; }
        [Required]
        public string? WatchNumber { get; set; }

    }
}
