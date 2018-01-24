using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.Models.ViewModels
{
    public class ProductViewModel
    {
        public int ProduktId { get; set; }
        public string ProduktNamn { get; set; }
        public bool IsSelected { get; set; }
    }
}
