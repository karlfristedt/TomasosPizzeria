using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.Models
{
    public interface IMatrattRepository
    {
        IQueryable<Matratt> GetAllMatratter();
        IQueryable<Kund> GetAllCustomers();
        IQueryable<Bestallning> GetAllOrders();

        IQueryable<Bestallning> GetOrdersById(int id);
        IQueryable<Kund> GetCustomersById(int id);
        IQueryable<Matratt> GetMatratterById(int id);
        
   

        void SaveOrder(Bestallning bestallning);

        //IQueryable<Produkt> Produkter { get; }
        //IQueryable<MatrattProdukt> MatrattProdukt { get; }
    }
}
