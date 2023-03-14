using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class SearchDriverViewModel
    {
        [Required]
        public string FullName { get; set; }
    }
}