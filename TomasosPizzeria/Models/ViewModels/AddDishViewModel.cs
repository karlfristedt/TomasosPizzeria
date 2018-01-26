using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TomasosPizzeria.Entities;

namespace TomasosPizzeria.Models.ViewModels
{
    public class AddDishViewModel
    {
        public AddDishViewModel()
        {
            Produkter = new List<ProductViewModel>();
        }

        [Required, MaxLength(50), Display(Name = "Namn")]
        public string MatrattNamn { get; set; }
        [Required, MaxLength(200), Display(Name = "Beskrivning")]
        public string Beskrivning { get; set; }
        [Required, Display(Name = "Pris")]
        public int Pris { get; set; }
        public string MatrattTyp { get; set; }
        
        public IEnumerable<SelectListItem> MatrattTyper { get; set; }
        public IList<ProductViewModel> Produkter { get; set; }
    }
}
