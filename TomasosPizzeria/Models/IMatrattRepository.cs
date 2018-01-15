using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.Models
{
    public interface IMatrattRepository
    {
        IQueryable<Matratt> GetMatratter();
        IQueryable<Kund> GetCustomers();
        IQueryable<Bestallning> GetOrders();
        void SaveOrder(Bestallning bestallning);

        //IQueryable<Produkt> Produkter { get; }
        //IQueryable<MatrattProdukt> MatrattProdukt { get; }
    }
}
