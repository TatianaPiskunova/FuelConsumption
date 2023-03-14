using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class EditCarFuelConsumptionViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public double FuelConsumption { get; set; }
    }
}
