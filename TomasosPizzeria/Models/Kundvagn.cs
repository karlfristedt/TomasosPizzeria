using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.Models
{
    public class Kundvagn
    {

        private List<Orderrad> radlista = new List<Orderrad>();

        public virtual void AddItem(Matratt matratt)
        {
            Orderrad rad = radlista
                .Where(p => p.Matratt.MatrattId == matratt.MatrattId)
                .FirstOrDefault();

            if (rad == null)
            {
                radlista.Add(new Orderrad
                {
                    Matratt = matratt
                });
            }
            else
            {
                rad.Antal++;
            }

        }

        public virtual void RemoveLine(Matratt matratt) =>
            radlista.RemoveAll(l => l.Matratt.MatrattId == matratt.MatrattId);

        public virtual decimal ComputeTotalValue() =>
            radlista.Sum(e => e.Matratt.Pris * e.Antal);

        public virtual void Clear() => radlista.Clear();

        public virtual IEnumerable<Orderrad> GetOrderrader()
        {
            return radlista;
        }
    }


}


