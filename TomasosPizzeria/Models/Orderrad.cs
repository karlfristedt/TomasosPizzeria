using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Entities;

namespace TomasosPizzeria.Models
{
    public class Orderrad
    {
        public int OrderradID { get; set; }
        public Matratt Matratt { get; set; }
        public int Antal { get; set; }
    }
}
