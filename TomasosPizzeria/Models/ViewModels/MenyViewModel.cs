using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Entities;

namespace TomasosPizzeria.Models.ViewModels
{
    public class MenyViewModel
    {
        //public MenyViewModel()
        //{
        //    Matratter = new List<MatrattTyp>;
        //}
        public ICollection<MatrattTyp> Matratter { get; set; }
    }
}
