using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TomasosPizzeria.Entities;

namespace TomasosPizzeria.Models
{
    public class Kundvagn
    {
        [JsonProperty]
        private List<Orderrad> radlista = new List<Orderrad>();

        public virtual void AddItem(Matratt matratt)
        {
            Orderrad rad = radlista.FirstOrDefault(p => p.Matratt.MatrattId == matratt.MatrattId);

            if (rad == null)
            {
                radlista.Add(new Orderrad
                {
                    Matratt = matratt,
                    Antal = 1
                });
            }
            else
            {
                rad.Antal++;
            }

        }

        public virtual void RemoveLine(Matratt matratt) =>
            radlista.RemoveAll(l => l.Matratt.MatrattId == matratt.MatrattId);

        public virtual int ComputeTotalValue() =>
            radlista.Sum(e => e.Matratt.Pris * e.Antal);

        public virtual void Clear() => radlista.Clear();

        public virtual IEnumerable<Orderrad> GetOrderrader()
        {
            return radlista;
        }
        public virtual int GetAntalRatter()
        {
            return radlista.Sum(x=>x.Antal);
        }
        public virtual int GetBilligastePizzan()
        {
            //var price = radlista.Where(n => n.Matratt.MatrattTypNavigation.Beskrivning == "Pizza")
            //    .OrderByDescending(pris => pris.Matratt.Pris).Last().Matratt.Pris;
            var price = radlista.Where(n => n.Matratt.MatrattTyp == 1) // Är Pizza ID
                .OrderByDescending(pris => pris.Matratt.Pris).Last().Matratt.Pris;
            return price;
        }
    }


}


