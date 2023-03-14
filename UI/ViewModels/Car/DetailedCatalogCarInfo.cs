using System.Collections.Generic;

namespace UI.ViewModels
{
    public class DetailedCatalogCarInfo
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public List<CarDriverViewModel> Drivers { get; set; }
        //public List<CarWatchViewModel> DriverWatch { get; set; }
       

    }
}