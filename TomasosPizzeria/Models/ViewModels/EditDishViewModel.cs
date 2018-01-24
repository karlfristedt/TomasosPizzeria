using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Entities;

namespace TomasosPizzeria.Models.ViewModels
{
    public class EditDishViewModel
    {
        public EditDishViewModel()
        {
            Produkter = new List<ProductViewModel>();
        }

        public int MatrattId { get; set; }
        [Required, MaxLength(50), Display(Name = "Namn")]
        public string MatrattNamn { get; set; }
        [Required, MaxLength(200), Display(Name = "Beskrivning")]
        public string Beskrivning { get; set; }
        [Required, Display(Name = "Pris")]
        public int Pris { get; set; }
        public string MatrattTyp { get; set; }

        public IList<ProductViewModel> Produkter { get; set; }
    }
}
