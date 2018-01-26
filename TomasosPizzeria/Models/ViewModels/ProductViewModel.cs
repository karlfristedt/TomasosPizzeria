using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.Models.ViewModels
{
    public class ProductViewModel
    {
        public int ProduktId { get; set; }
        [Required, MaxLength(50)]
        public string ProduktNamn { get; set; }
        public bool IsSelected { get; set; }
    }
}
