﻿using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class DriverDeleteViewModel
    {
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string FullNumber { get; set; }
    }
}
