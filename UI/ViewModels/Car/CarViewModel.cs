using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public int YearProduct { get; set; }
        [Required]
        public double FuelConsumption { get; set; }

    }
}
